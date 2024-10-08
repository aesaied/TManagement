﻿namespace TManagement.AppServices
{
    public class AppResult
    {

        public bool Success { get; set; }

        public string[] Errors { get; set; } = [];

        public static AppResult Ok() { return new AppResult { Success = true, }; }
    }

    public class AppResult<T> : AppResult
    {
        public T? Value { get; set; }

    }
}
