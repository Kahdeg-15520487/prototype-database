using prototype_database.Contract.DTOs;
using System.Collections.Generic;

namespace prototype_database.Contract
{
    public interface IUserService
    {
        /// <summary>
        /// Get all Users
        /// </summary>
        ICollection<UserDTO> GetUsers();
        /// <summary>
        /// Get user with the given id
        /// </summary>
        /// <param name="id">user's id</param>
        /// <returns>null if user is not found</returns>
        UserDTO GetUser(string id);
        /// <summary>
        /// Create a new user, user's id will be generated
        /// </summary>
        /// <param name="dto">user's info</param>
        /// <returns>user's id if success, else the error message</returns>
        string Create(UserDTO dto);
        /// <summary>
        /// Update a user's info
        /// </summary>
        /// <param name="dto">user's new info</param>
        /// <returns>user's id if success, else the error message</returns>
        string Update(UserDTO dto);
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">user's id</param>
        /// <returns>true if successful</returns>
        bool Delete(string id);
    }
}
