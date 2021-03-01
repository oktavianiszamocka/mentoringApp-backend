
using MentorApp.DTOs.Requests;
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
                                            IdUser = comment.CreatedBy,
                                            firstName = comment.CreatedByNavigation.FirstName,
                                            lastName = comment.CreatedByNavigation.LastName,
                                            imageUrl = comment.CreatedByNavigation.Avatar
                                        }
                                    })
                                    .ToList();
            return commentWrapperList;
        }
        public async Task<Post> SaveNewPost(NewPostDTO newPost)
        {

            var post = ConvertPostRequestDTOToPost(newPost);
            var saveNewPost = await _postRepository.SaveNewPost(post);
            await mappingTag(newPost.Tags, saveNewPost.IdPost);
            return saveNewPost;
        }
        public async Task<Post> UpdatePost(EditPostDTO newPost)
        {
            var post = new Post
            {
                IdPost = newPost.IdPost,
                Title = newPost.Title,
                Content = newPost.Content,
                Attachment = newPost.Attachment

            };
            post = await _postRepository.UpdatePost(post);

            var tagsList = await _postRepository.GetAllPostTagByPostId(post.IdPost);
            List<string> nonExistingPostTag = new List<string>();
            foreach (string tag in newPost.Tags)
            {
                var existingPostTag = tagsList.Find(posttag => posttag.TagNavigation.Name.Equals(tag));
                if (existingPostTag == null)
                {
                    nonExistingPostTag.Add(tag);
                 
                }
            }
            await mappingTag(nonExistingPostTag, post.IdPost);
          
            return post;
        }

        public async Task<Comment> SaveNewComment(Comment comment)
        {
            return await _postRepository.SaveNewComment(comment);
        }

        public async Task<Post> DeletePost(int IdPost)
        {
            var allPostTagsOfPost = await _postRepository.GetAllPostTagByPostId(IdPost);
            foreach(PostTag postTag in allPostTagsOfPost)
            {
                await _postRepository.DeletePostTag(postTag.IdPostTag);
            }

            return await _postRepository.DeletePost(IdPost);
        }

        public async Task<List<string>> mappingTag(List<string> tags, int IdPost)
        {
            foreach (string tag in tags)
            {
                var existingTag = await _postRepository.GetTagByName(tag);
                if (existingTag != null)
                {

                    var newPostTag = new PostTag
                    {
                        Post = IdPost,
                        Tag = existingTag.IdTag
                    };
                    var saveNewPostTag = await _postRepository.SaveNewPostTag(newPostTag);

                }
                else
                {
                    var newTag = new Tag
                    {
                        Name = tag
                    };

                    var saveNewTag = await _postRepository.SaveNewTag(newTag);

                    var newPostTag = new PostTag
                    {
                        Post = IdPost,
                        Tag = saveNewTag.IdTag
                    };
                    var saveNewPostTag = await _postRepository.SaveNewPostTag(newPostTag);

                }
            }
            return tags;
        }


        public Post ConvertPostRequestDTOToPost(NewPostDTO newPost)
        {
            var post = new Post
            {
                Title = newPost.Title,
                Content = newPost.Content,
                DateOfPublication = newPost.DateOfPublication,
                Writer = newPost.Writer,
                Project = newPost.Project,
                Attachment = newPost.Attachment

            };

            return post;
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
                                        IdUser = post.Writer,
                                        firstName = post.WriterNavigation.FirstName,
                                        lastName = post.WriterNavigation.LastName,
                                        imageUrl = post.WriterNavigation.Avatar

                                    },
                                    hasMoreThanOneComment = post.Comment.Count() > 1 ? true : false,
                                    NewestComment = post.Comment.OrderByDescending(comment => comment.CreatedOn).Select(comment => new CommentWrapper
                                    {
                                        IdComment = comment.IdComment,
                                        Comment = comment.Comment1,
                                        CreatedOn = comment.CreatedOn,
                                        CreatedBy = new UserWrapper
                                        {
                                            IdUser = comment.CreatedBy,
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

        public async Task<Comment> DeleteComment(int IdComment)
        {
            return await _postRepository.DeleteComment(IdComment);
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            return await _postRepository.UpdateComment(comment);
        }
    }
}
