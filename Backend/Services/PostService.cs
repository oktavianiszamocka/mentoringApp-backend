using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
     
        public async Task<List<PostWrapper>> GetAll()
        {
            var allPost =  await _postRepository.GetAllPost();
            var allPostWrapper = GetPostWrappers(allPost);
            return allPostWrapper;
        }

        public async Task<List<PostWrapper>> GetGeneralPost()
        {
            var postGeneral = await _postRepository.GetGeneralPost();
            var postWrapperList = GetPostWrappers(postGeneral);
            return postWrapperList;
        }

        public async Task<List<PostWrapper>> GetPostProject(int IdProject)
        {

            var postProject = await _postRepository.GetPostByProject(IdProject);
            var postWrapperList = GetPostWrappers(postProject);
            return postWrapperList;
        }


        public async Task<List<CommentWrapper>> GetAllCommentByPostId(int IdPost)
        {
            var commentsPost = await _postRepository.GetAllCommentByPostId(IdPost);
            var commentWrapperList = commentsPost.Comment
                                    .OrderByDescending(comment => comment.CreatedOn)
                                    .Select(comment => new CommentWrapper
                                    {

                                        IdComment = comment.IdComment,
                                        Comment = comment.Comment1,
                                        CreatedOn = comment.CreatedOn,
                                        CreatedBy = new UserWrapper
                                        {
                                            firstName = comment.CreatedByNavigation.FirstName,
                                            lastName = comment.CreatedByNavigation.LastName,
                                            imageUrl = comment.CreatedByNavigation.Avatar
                                        }
                                    })
                                    .ToList();
            return commentWrapperList;
        }
        public async Task<Post> SaveNewPost(Post post)
        {
            return await  _postRepository.SaveNewPost(post);
        }
        public List<PostWrapper> GetPostWrappers(List<Post> postList)
        {
            var postWrapperList = postList
                                .Select(post => new PostWrapper
                                {
                                    IdPost = post.IdPost,
                                    Title = post.Title,
                                    Content = post.Content,
                                    DateOfPublication = post.DateOfPublication,
                                    Writer = new UserWrapper
                                    {
                                        firstName = post.WriterNavigation.FirstName,
                                        lastName = post.WriterNavigation.LastName,
                                        imageUrl = post.WriterNavigation.Avatar

                                    },
                                    NewestComment = post.Comment.OrderByDescending(comment => comment.CreatedOn).Select(comment => new CommentWrapper
                                    {
                                        IdComment = comment.IdComment,
                                        Comment = comment.Comment1,
                                        CreatedOn = comment.CreatedOn,
                                        CreatedBy = new UserWrapper
                                        {
                                            firstName = comment.CreatedByNavigation.FirstName,
                                            lastName = comment.CreatedByNavigation.LastName,
                                            imageUrl = comment.CreatedByNavigation.Avatar
                                        }
                                    }).FirstOrDefault(),
                                    tags = post.PostTag.Select(postTag => new string
                                    (postTag.TagNavigation.Name

                                    )).ToList()
                                })

                             .ToList();
            return postWrapperList;
        }


    }
}
