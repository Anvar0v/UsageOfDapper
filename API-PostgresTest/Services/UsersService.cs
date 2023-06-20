using API_PostgresTest.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_PostgresTest.Services;
public class UsersService
{
    public async Task<bool> AddUserAsync(NpgsqlConnection connection, User user)
    {
        var isExistingUser = await GetUserByIdAsync(connection, user.id);

        if (isExistingUser != null)
            return false;

        //! if there is no such user in db with id that passed as parameter than we have to add the user;
        var query = "insert into users (id,name,surname,age) values(@id,@name,@surname,@age);";

        var queryArgs = new
        {
            id = user.id,
            name = user.name,
            surname = user.surname,
            age = user.age,
        };

        await connection.ExecuteAsync(query, queryArgs);
        return true;
    }

    public async Task DeleteUserByIdAsync(NpgsqlConnection connection, int userId)
    {
        var user = await GetUserByIdAsync(connection, userId);

        if (user != null)
            await connection.ExecuteAsync($"delete from users where id={userId}");
        else
            throw new Exception($"There is no such user with id {userId}");
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(NpgsqlConnection connection)
        => await connection.QueryAsync<User>("select * from users limit 20;");

    public async Task<User> GetUserByIdAsync(NpgsqlConnection connection, int userId)
        => await connection.QueryFirstOrDefaultAsync<User>($"select * from users where id={userId}");
    
    public async Task<bool> UpdateUserAsync(NpgsqlConnection connection,User user, int userId)
    {
        var existingUser = await GetUserByIdAsync(connection, userId);

        if (existingUser is null)
            return false;

        var queryArgs = new
        {
            id = user.id,
            name = user.name,
            surname = user.surname,
            age = user.age
        };
        await connection.ExecuteAsync($"update users set id=@id,name=@name,surname=@surname,age=@age where id={userId}",queryArgs);
        return true;
    }

}
