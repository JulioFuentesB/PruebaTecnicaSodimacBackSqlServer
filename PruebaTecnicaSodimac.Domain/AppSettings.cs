// <copyright file="AppSettings.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Domain;

public class AppSettings
{
	public ConnectionStrings ConnectionStrings { get; set; } = null!;
	public Logging Logging { get; set; } = null!;
	public string AllowedHosts { get; set; } = null!;
	public bool EnableRequestResponseLogging { get; set; }
	public List<string> WithOrigins { get; set; } = null!;
}

public class ConnectionStrings
{
	public string SecretDB { get; set; } = null!;
}

public class Logging
{
	public LogLevel? LogLevel { get; set; }
}

public class LogLevel
{
	public string? Default { get; set; }

	//[JsonProperty("Microsoft.AspNetCore")]
	public string? MicrosoftAspNetCore { get; set; }
}
