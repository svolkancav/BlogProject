using BlogProject.Application.Models.DTOs;
using BlogProject.Application.Models.VMs;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;


namespace BlogProject.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<CreateAuthorDTO> CreateAuthor()
        {
            CreateAuthorDTO createAuthorDTO = new CreateAuthorDTO();
            return createAuthorDTO;
            
        }

        public async Task Delete(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.ID == id);
            author.DeleteDate = DateTime.Now;
            author.Status = Domain.Enum.Status.Passive;
            await _authorRepository.Delete(author);

        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            var authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM
                {
                    Id = x.ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,

                },
                where: x => x.Status != Domain.Enum.Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName)

                );
            return authors;
        }

        public async Task<UpdateAuthorDTO> GetByID(int id)
        {
            Author author = await _authorRepository.GetDefault(x => x.ID == id);
            UpdateAuthorDTO authorDTO = new UpdateAuthorDTO()
            {
                ID = author.ID,
                ImagePath = author.ImagePath,
                FirstName = author.FirstName,
                LastName = author.LastName,

            };
            return authorDTO;
        }

        public async Task Register(CreateAuthorDTO model)
        {
            Author author = new Author()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImagePath= model.ImagePath,

            };
            if (model.UploadPath != null)
            {
                Image image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(200, 200));

                Guid guid = new Guid();
                image.Save($"wwwroot/images/{guid}");
                author.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                author.ImagePath = $"/images/defaultauth.jpg";
            }
            await _authorRepository.Create(author);
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            Author author = new Author()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ID = model.ID,

            };

            if (model.UploadPath != null)
            {
                // using SixLabors.ImageSharp;
                Image image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(200, 200));

                Guid guid = new Guid();
                image.Save($"wwwroot/images/{guid}"); //foler'ın altına kaydettim.

                author.ImagePath = $"/images/{guid}.jpg";
            }
            else
                author.ImagePath = $"/images/defaultAuth.jpg";

            await _authorRepository.Update(author);
        }
    }
}
