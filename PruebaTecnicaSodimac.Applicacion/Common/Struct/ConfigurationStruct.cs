// <copyright file="ConfigurationStruct.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Application.Common.Struct;

public struct ConfigurationStruct
{
	public const string DbSecretDB = "SGLPROD";
	public const bool CreateSuccessfulIsSuccess = true;
	public const bool CreateSuccessfulIsError = false;
	public const bool CreateUnsuccessfulIsSuccess = false;
	public const bool CreateUnsuccessfulIsError = false;
	public const bool CreateErrorfulIsSuccess = false;
	public const bool CreateErrorIsError = true;
	public const string _contentTypeSuport = "application/json";
	public const string _contentTypeXmlSuport = "application/xml";
	public const string Accept = "Accept";

	public const string HeadersOcpApimSubscriptionKey =
		"Ocp-Apim-Subscription-Key";

	public const string sentenseDbOracle = "select ora_database_name from dual";
	public const string OraDatabaseName = "ORA_DATABASE_NAME";
	public const string SiKeyVault = "S";

	public const string TermsOfServices =
		"https://example.com/terms-of-service";

	public const string Contact = "https://example.com/contact";
	public const string License = "https://example.com/license";
	public const string SecretDb = "SecretDB";
	public const string WithOrigins = "WithOrigins";
	public const string CorsPolicy = "CorsPolicy";
	public const string RealIpHeader = "X-Real-IP";
	public const bool EnableEndpointRateLimiting = true;
	public const bool StackBlockedRequests = false;
	public const string Endpoint = "*";
	public const string Period = "1s";
	public const string HttpRequestExceptionMessage =
		"Ha ocurrido un error en el consumo del servicio. Status code: {0} {1} Content: {2}";

	public const string Gerencia = "Gerencia";
	public const string Celula = "Celula";
	public const string Aplicacion = "Aplicacion";
	public const string Proyecto = "Proyecto";

}
