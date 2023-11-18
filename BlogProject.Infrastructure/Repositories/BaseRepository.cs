﻿using BlogProject.Domain.Entities;
using BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly AppDbContext _context;
        protected DbSet<T> table;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            await _context.SaveChangesAsync(); // servis katmanında entity'sine göre pasif hale getireceğiz.
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>
            (
                Expression<Func<T, TResult>> select, 
                Expression<Func<T, bool>> where, 
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> include = null
            )

        {
            IQueryable<T> query = table; // select * from Post
            if (where != null)
            {
                query = query.Where(where); // select * from Post where GenreId=3
            }
            if (include != null)
            {
                query = include(query); // join
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            }
            else
                return await query.Select(select).FirstOrDefaultAsync();

        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table; // select * from Post
            if (where != null)
            {
                query = query.Where(where); // select * from Post where GenreId=3
            }
            if (include != null)
            {
                query = include(query); // join
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
                return await query.Select(select).ToListAsync();
        }

        public async Task Update(T entity)
        {
            //_context.Update(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
