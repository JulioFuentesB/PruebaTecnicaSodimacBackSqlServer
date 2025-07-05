// <copyright file="SerilogImplements.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using Microsoft.Extensions.Logging;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services.Serilog;
using System.Runtime.CompilerServices;

namespace PruebaTecnicaSodimac.Application.Services.Serilog;

public class SerilogImplements : ISerilogImplements
{
    #region private members

    private readonly ILogger<SerilogImplements> _logger;

    #endregion

    #region constructors

    public SerilogImplements(ILogger<SerilogImplements> logger)
    {
        _logger = logger;
    }

    #endregion

    #region public methods
    /// <summary>
    /// Metodo para procesar los metodos por defecto de serilog Sodimac
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <param name="message"></param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns></returns>
    public string? ObtainMessageDefault(string messageType, string method,
        string? parameters, string? message, [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {

        //var logErrorSodimacOptions = new LogErrorSodimacOptions
        //{
        //	MemberName = memberName,
        //	SourceFilePath = sourceFilePath,
        //	SourceLineNumber = sourceLineNumber
        //};
        //switch (messageType)
        //{
        //	case ConfigurationMessageType.Error:
        //		logErrorSodimacOptions.Message = message;
        //		logErrorSodimacOptions.EventId = new EventId(1);
        //		logErrorSodimacOptions.Args = new[] { ConfigurationMessageType.Error, method, parameters, message };
        //		_logger.LogErrorSodimac(logErrorSodimacOptions);
        //		break;
        //	case ConfigurationMessageType.Critical:
        //		logErrorSodimacOptions.Message = message;
        //		logErrorSodimacOptions.EventId = new EventId(2);
        //		logErrorSodimacOptions.Args = new[] { ConfigurationMessageType.Critical, method, parameters, message };
        //		_logger.LogCriticalSodimac(logErrorSodimacOptions);
        //		break;
        //	case ConfigurationMessageType.Warning:
        //		logErrorSodimacOptions.Message = message;
        //		logErrorSodimacOptions.EventId = new EventId(3);
        //		logErrorSodimacOptions.Args = new[] { ConfigurationMessageType.Warning, method, parameters, message };
        //		_logger.LogWarningSodimac(logErrorSodimacOptions);
        //		break;
        //	case ConfigurationMessageType.Information:
        //		logErrorSodimacOptions.Message = message;
        //		logErrorSodimacOptions.EventId = new EventId(4);
        //		logErrorSodimacOptions.Args = new[] { ConfigurationMessageType.Information, method, parameters, message };
        //		_logger.LogInformationSodimac(logErrorSodimacOptions);
        //		break;
        //}

        return message;
    }

    #endregion
}
