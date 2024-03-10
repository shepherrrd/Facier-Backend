﻿namespace AttendanceCapture.Models;

public class Dto
{
}

public class BaseResponse
{
    public bool Status { get; set; }
    public string? Message { get; set; }

    public BaseResponse(bool status, string message)
    {
        Status = status;
        Message = message;
    }
}
public class ValidationResultModel
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}

public class BaseResponse<T>
{
    public bool Status { get; set; }
    public string? Message { get; set; }


    public T? Data { get; set; }
    public BaseResponse()
    {

    }
    public BaseResponse(bool status, string message)
    {
        Status = status;
        Message = message;
    }
    public BaseResponse(bool status, string message, T data)
    {
        Status = status;
        Message = message;
        Data = data;
    }
}
