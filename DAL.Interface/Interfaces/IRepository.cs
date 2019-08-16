using System;
using System.Collections.Generic;

namespace DAL.Interface.Interfaces
{
    /// <summary>
    /// Contains methods for work with data storage.
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <returns>List of items <see cref="T"/>.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get <see cref="T"/> element by id.
        /// </summary>
        /// <param name="id">Id of eleemnt.</param>
        /// <returns>Element of <see cref="T"/> type.</returns>
        T GetById(int id);

        /// <summary>
        /// Get <see cref="T"/> element by specified parameter.
        /// </summary>
        /// <param name="parameter">Parameter name.</param>
        /// <param name="value">Value of specified parameter.</param>
        /// <returns>>Element of <see cref="T"/> type.</returns>
        IEnumerable<T> GetByParameter(string parameter, string value);

        /// <summary>
        /// Create element <see cref="T"/> in repository.
        /// </summary>
        /// <param name="item"><see cref="T"/> item.</param>
        /// <returns>Id of created item.</returns>
        string Create(T item);

        /// <summary>
        /// Update element <see cref="T"/> in repository.
        /// </summary>
        /// <param name="item"><see cref="T"/> item.</param>
        void Update(T item);

        /// <summary>
        /// Update element <see cref="T"/> from repository.
        /// </summary>
        /// <param name="item"><see cref="T"/> item.</param>
        void Delete(int id);
    }
}
