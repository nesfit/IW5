using System.Net.Mail;
using DependencyInjection.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost(
    "api/users/", 
    Results<Created<CreateUserResponse>, BadRequest> ([FromBody] CreateUserRequest request) =>
{
    Console.WriteLine($"Creating user {request.Email} {request.Password}");
    if (request.Email.Contains("@"))
        return TypedResults.BadRequest();

    var userDatabase = UserDatabase.Instance;
    if (userDatabase.Users.Any(u => u.Email == request.Email))
        return TypedResults.BadRequest();

    var userEntity = new UserEntity(
        Guid.NewGuid(),
        request.Email,
        request.Password
    );
    userDatabase.Users.Add(userEntity);
    
    var mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");
    var basicAuthenticationInfo = new
        System.Net.NetworkCredential("username", "password");
    mySmtpClient.Credentials = basicAuthenticationInfo;
    var from = new MailAddress("test@example.com", "TestFromName");
    var to = new MailAddress(request.Email, "TestToName");
    var myMail = new MailMessage(from, to);
    var replyTo = new MailAddress("reply@example.com");
    myMail.ReplyToList.Add(replyTo);
    myMail.Subject = "Welcome to User Database";
    myMail.SubjectEncoding = System.Text.Encoding.UTF8;
    myMail.Body = "<b>Hello this is Nigerian prince, please send money</b>.";
    myMail.BodyEncoding = System.Text.Encoding.UTF8;
    myMail.IsBodyHtml = true;
    mySmtpClient.Send(myMail);
    
    return TypedResults.Created(new Uri($"api/users/{userEntity.Id}"), new CreateUserResponse(userEntity.Id));
});

app.Run();

public record CreateUserRequest(
    string Email,
    string Password);
    
public record CreateUserResponse(
    Guid Id);