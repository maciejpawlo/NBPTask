using NBPTask.Shared.Results;

namespace NBPTask.Application.Errors;

public record IncorrectPasswordOrUserNameError() : BadRequestError("username_or_password_incorrect", "given username or password is incorrect");