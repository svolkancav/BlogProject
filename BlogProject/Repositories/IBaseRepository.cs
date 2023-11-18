using BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity); // veritabanında silme işlemi yapmayacağım, statüsünü pasife çekeceğim.
        Task<bool> Any(Expression<Func<T, bool>> expression); // kayıt varsa true, yoksa false döner.
        Task<T> GetDefault(Expression<Func<T, bool>> expression); // dinamik olarak where işlemi sağlar.
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression); //GenreId=5 olan Post'ların hepsini döndür.

        //Tek Dönecek

        //Select, where, sıralama, join
        //Hewm select, hem order by yapabileceğiz. Post, Author, comment, Like'ları birlikte çekmek için include etmek gerekir. Bir sorguya birden fazla tablo girecek yani eagerLoading kullanacağız.
        Task<TResult> GetFilteredFirstOrDefault<TResult>
            (
                Expression<Func<T, TResult>> select, // select
                Expression<Func<T, bool>> where, // where
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //sıralama
                Func<IQueryable<T>, IIncludableQueryable<T,object>> include = null //join
            );

        //Çoklu dönecek
        Task<List<TResult>> GetFilteredList<TResult>
            (
                Expression<Func<T, TResult>> select, // select
                Expression<Func<T, bool>> where, // where
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, //sıralama
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null //join
            );

        //Task<IEnumerable<T>> GetAll();

    }
}
