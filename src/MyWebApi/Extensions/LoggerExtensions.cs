// <copyright file="LoggerExtensions.cs" company="Ken Wong">
// Copyright (c) Ken Wong.  All rights reserved.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace MyWebApi.Extensions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// Logger extension methods.
/// </summary>
public static partial class LoggerExtensions
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Starting {Application}.")]
    public static partial void LogApplicationStarting(this ILogger logger, string application);

    [LoggerMessage(Level = LogLevel.Information, Message = "{Application} Stopped.")]
    public static partial void LogApplicationStopped(this ILogger logger, string application);

    [LoggerMessage(Level = LogLevel.Critical, Message = "{Application} Failed to Start.")]
    public static partial void LogCriticalError(this ILogger logger, string application, Exception ex);
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
