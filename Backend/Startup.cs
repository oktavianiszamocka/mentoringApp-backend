using AutoMapper;
using MentorApp.Persistence;
using MentorApp.Repository;
using MentorApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;

namespace MentorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MentorAppContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = "https://localhost:5001", //should come from configuration
                            ValidAudience = "https://localhost:5001", //should come from configuration
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = (context) =>
                            {
                                if (context.Exception is SecurityTokenExpiredException)
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPersonalNoteRepository, PersonalNoteRepository>();
            services.AddTransient<IPersonalNoteService, PersonalNoteService>();
            services.AddTransient<IProjectMemberRepository, ProjectMemberRepository>();
            services.AddTransient<IProjectMemberService, ProjectMemberService>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProjectPromotersRepository, ProjectPromotersRepository>();
            services.AddTransient<IProjectPromoterService, ProjectPromoterService>();

            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IMilestoneRepository, MilestoneRepository>();
            services.AddTransient<IMilestoneService, MilestoneService>();
            services.AddTransient<IInvitationRepository, InvitationRepository>();
            services.AddTransient<IInvitationService, InvitationService>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITaskService, TaskService>();


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}