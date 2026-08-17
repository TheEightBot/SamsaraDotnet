namespace Samsara.Sdk.Models.Beta;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Details of a Function.
/// Returned by <c>GET /functions/{name}</c> (operationId <c>getFunction</c>, beta).
/// Mirrors the spec schema <c>GetFunctionDetailResponseBody</c>.
/// </summary>
public sealed record FunctionDetail
{
    /// <summary>Code package status for the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("codePackage")]
    public FunctionCodePackage? CodePackage { get; init; }

    /// <summary>Configuration of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("config")]
    public FunctionConfig? Config { get; init; }

    /// <summary>
    /// RFC 3339 timestamp when the Function was created. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Description of the Function.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Computed effects of the Function configuration. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("effects")]
    public FunctionEffects? Effects { get; init; }

    /// <summary>
    /// Epoch milliseconds of the last update. Use this value for optimistic locking in
    /// PATCH requests. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("lastUpdateTimestampMs")]
    public long? LastUpdateTimestampMs { get; init; }

    /// <summary>Name of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// RFC 3339 timestamp when the Function was last updated. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }
}

/// <summary>
/// Details of a newly created Function, including a URL for uploading the code package. After
/// uploading, call the deploy endpoint to make the function runnable.
/// Returned by <c>POST /functions</c> (operationId <c>createFunction</c>, beta).
/// Mirrors the spec schema <c>CreateFunctionDetailResponseBody</c>.
/// </summary>
public sealed record FunctionCreateDetail
{
    /// <summary>Code package status for the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("codePackage")]
    public FunctionCodePackage? CodePackage { get; init; }

    /// <summary>Configuration of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("config")]
    public FunctionConfig? Config { get; init; }

    /// <summary>
    /// RFC 3339 timestamp when the Function was created. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("createdAtTime")]
    public DateTimeOffset? CreatedAtTime { get; init; }

    /// <summary>Description of the Function.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Computed effects of the Function configuration. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("effects")]
    public FunctionEffects? Effects { get; init; }

    /// <summary>
    /// Epoch milliseconds of the last update. Use this value for optimistic locking in
    /// PATCH requests. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("lastUpdateTimestampMs")]
    public long? LastUpdateTimestampMs { get; init; }

    /// <summary>Name of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// RFC 3339 timestamp when the Function was last updated. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

    /// <summary>
    /// A presigned PUT URL for uploading the function's code package. Valid for a limited
    /// time. After uploading, call `POST /functions/{name}/deploy` to make the function
    /// runnable. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("uploadPutUrl")]
    public string? UploadPutUrl { get; init; }
}

/// <summary>
/// Details of an updated Function, including a URL for uploading a new code package. After
/// uploading, call the deploy endpoint for the changes to be applied.
/// Returned by <c>PATCH /functions/{name}</c> (operationId <c>patchFunction</c>, beta).
/// Mirrors the spec schema <c>PatchFunctionDetailResponseBody</c>.
/// </summary>
public sealed record FunctionUpdateDetail
{
    /// <summary>
    /// Epoch milliseconds of the last update. Use this value in subsequent patch requests.
    /// Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("lastUpdateTimestampMs")]
    public long? LastUpdateTimestampMs { get; init; }

    /// <summary>Name of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// RFC 3339 timestamp when the Function was last updated. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("updatedAtTime")]
    public DateTimeOffset? UpdatedAtTime { get; init; }

    /// <summary>
    /// A presigned PUT URL for uploading the function's code package. Valid for a limited
    /// time. After uploading, call `POST /functions/{name}/deploy` for the changes to be
    /// applied. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("uploadPutUrl")]
    public string? UploadPutUrl { get; init; }
}

/// <summary>
/// Code package status for the Function.
/// Mirrors the spec schema <c>FunctionCodePackageResponseBody</c>.
/// </summary>
public sealed record FunctionCodePackage
{
    /// <summary>
    /// A presigned GET URL for downloading the function's current code package. Present
    /// only when code has been uploaded.
    /// </summary>
    [JsonPropertyName("downloadGetUrl")]
    public string? DownloadGetUrl { get; init; }

    /// <summary>
    /// Status of the code package. Valid values: `unknown`, `pendingUpload`, `uploaded`,
    /// `deployed`. One of: <c>unknown</c>, <c>pendingUpload</c>, <c>uploaded</c>,
    /// <c>deployed</c>. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// Configuration of the Function.
/// Mirrors the spec schema <c>FunctionConfigResponseBody</c>.
/// </summary>
public sealed record FunctionConfig
{
    /// <summary>
    /// The handler entrypoint for the function code. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("handler")]
    public string? Handler { get; init; }

    /// <summary>Whether the run schedule is enabled. Spec marks this required on the response.</summary>
    [JsonPropertyName("isScheduleEnabled")]
    public bool? IsScheduleEnabled { get; init; }

    /// <summary>Default parameters for the function. Spec marks this required on the response.</summary>
    [JsonPropertyName("params")]
    public IReadOnlyDictionary<string, string>? Params { get; init; }

    /// <summary>Schedule configuration for a Function.</summary>
    [JsonPropertyName("schedule")]
    public FunctionSchedule? Schedule { get; init; }

    /// <summary>
    /// Secret names available to the function. Values are always empty. Spec marks this
    /// required on the response.
    /// </summary>
    [JsonPropertyName("secrets")]
    public IReadOnlyDictionary<string, string>? Secrets { get; init; }
}

/// <summary>
/// Schedule configuration for a Function.
/// Mirrors the spec schema <c>FunctionScheduleResponseBody</c>.
/// </summary>
public sealed record FunctionSchedule
{
    /// <summary>
    /// Schedule entries defining when the function runs. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("entries")]
    public IReadOnlyList<FunctionScheduleEntry>? Entries { get; init; }

    /// <summary>IANA timezone name for the schedule. Spec marks this required on the response.</summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }
}

/// <summary>
/// A single schedule entry defining when a Function runs.
/// Mirrors the spec schema <c>FunctionScheduleEntryResponseBody</c>.
/// </summary>
public sealed record FunctionScheduleEntry
{
    /// <summary>
    /// Days of the week this entry applies to (7 elements, index 0 = Sunday). Spec marks
    /// this required on the response.
    /// </summary>
    [JsonPropertyName("daysOfWeek")]
    public IReadOnlyList<bool>? DaysOfWeek { get; init; }

    /// <summary>
    /// Time since midnight in milliseconds when the function runs. Spec marks this required
    /// on the response.
    /// </summary>
    [JsonPropertyName("timeSinceMidnightMs")]
    public long? TimeSinceMidnightMs { get; init; }
}

/// <summary>
/// Computed effects of the Function configuration.
/// Mirrors the spec schema <c>FunctionEffectsResponseBody</c>.
/// </summary>
public sealed record FunctionEffects
{
    /// <summary>RFC 3339 timestamp of the next scheduled run. Omitted if no schedule is set.</summary>
    [JsonPropertyName("nextScheduledAtTime")]
    public DateTimeOffset? NextScheduledAtTime { get; init; }
}

/// <summary>
/// Request body for creating a Function.
/// Request body for <c>POST /functions</c> (operationId <c>createFunction</c>, beta).
/// Mirrors the spec schema <c>FunctionsCreateFunctionRequestBody</c>.
/// </summary>
public sealed record CreateFunctionRequest
{
    /// <summary>Configuration for the new Function. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("config")]
    public required CreateFunctionConfig Config { get; init; }

    /// <summary>A description of the Function.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Unique name for the Function. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>
/// Configuration for the new Function.
/// Mirrors the spec schema <c>CreateFunctionRequestConfigRequestBody</c>.
/// </summary>
public sealed record CreateFunctionConfig
{
    /// <summary>
    /// The handler entrypoint for the function code (e.g. 'index.handler'). Spec marks this
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("handler")]
    public required string Handler { get; init; }

    /// <summary>Whether the run schedule is enabled.</summary>
    [JsonPropertyName("isScheduleEnabled")]
    public bool? IsScheduleEnabled { get; init; }

    /// <summary>Default parameter values for the function.</summary>
    [JsonPropertyName("params")]
    public IReadOnlyDictionary<string, string>? Params { get; init; }

    /// <summary>Schedule configuration for a Function.</summary>
    [JsonPropertyName("schedule")]
    public FunctionScheduleInput? Schedule { get; init; }

    /// <summary>Secrets available to the function at runtime.</summary>
    [JsonPropertyName("secrets")]
    public IReadOnlyDictionary<string, string>? Secrets { get; init; }
}

/// <summary>
/// Request body for updating a Function.
/// Request body for <c>PATCH /functions/{name}</c> (operationId <c>patchFunction</c>, beta).
/// Mirrors the spec schema <c>FunctionsPatchFunctionRequestBody</c>.
/// </summary>
public sealed record UpdateFunctionRequest
{
    /// <summary>
    /// Configuration fields to update on the Function. All fields are optional; only
    /// provided fields are updated.
    /// </summary>
    [JsonPropertyName("config")]
    public UpdateFunctionConfig? Config { get; init; }

    /// <summary>A description of the Function.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// Timestamp of the last known update to this Function, obtained from a create or get
    /// response. Required to prevent conflicting updates. Spec marks this REQUIRED.
    /// </summary>
    [JsonPropertyName("lastUpdateTimestampMs")]
    public required long LastUpdateTimestampMs { get; init; }
}

/// <summary>
/// Configuration fields to update on the Function. All fields are optional; only provided
/// fields are updated.
/// Mirrors the spec schema <c>PatchFunctionRequestConfigRequestBody</c>.
/// </summary>
public sealed record UpdateFunctionConfig
{
    /// <summary>The handler entrypoint for the function code (e.g. 'index.handler').</summary>
    [JsonPropertyName("handler")]
    public string? Handler { get; init; }

    /// <summary>Whether the run schedule is enabled.</summary>
    [JsonPropertyName("isScheduleEnabled")]
    public bool? IsScheduleEnabled { get; init; }

    /// <summary>Default parameter values for the function. Replaces all existing parameters.</summary>
    [JsonPropertyName("params")]
    public IReadOnlyDictionary<string, string>? Params { get; init; }

    /// <summary>Schedule configuration for a Function.</summary>
    [JsonPropertyName("schedule")]
    public FunctionScheduleInput? Schedule { get; init; }

    /// <summary>Secrets available to the function at runtime. Replaces all existing secrets.</summary>
    [JsonPropertyName("secrets")]
    public IReadOnlyDictionary<string, string>? Secrets { get; init; }
}

/// <summary>
/// Schedule configuration for a Function.
/// Mirrors the spec schema <c>FunctionScheduleRequestBody</c>.
/// </summary>
public sealed record FunctionScheduleInput
{
    /// <summary>Schedule entries defining when the function runs. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("entries")]
    public required IReadOnlyList<FunctionScheduleEntryInput> Entries { get; init; }

    /// <summary>IANA timezone name for the schedule. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("timezone")]
    public required string Timezone { get; init; }
}

/// <summary>
/// A single schedule entry defining when a Function runs.
/// Mirrors the spec schema <c>FunctionScheduleEntryRequestBody</c>.
/// </summary>
public sealed record FunctionScheduleEntryInput
{
    /// <summary>
    /// Days of the week this entry applies to (7 elements, index 0 = Sunday). Spec marks
    /// this REQUIRED.
    /// </summary>
    [JsonPropertyName("daysOfWeek")]
    public required IReadOnlyList<bool> DaysOfWeek { get; init; }

    /// <summary>
    /// Time since midnight in milliseconds when the function runs. Spec marks this
    /// REQUIRED.
    /// </summary>
    [JsonPropertyName("timeSinceMidnightMs")]
    public required long TimeSinceMidnightMs { get; init; }
}

/// <summary>
/// Details of a deployed Function.
/// Returned by <c>POST /functions/{name}/deploy</c> (operationId <c>deployFunction</c>, beta).
/// Mirrors the spec schema <c>DeployFunctionDetailResponseBody</c>.
/// </summary>
public sealed record FunctionDeployResult
{
    /// <summary>Name of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// Request body for starting a Function run.
/// Request body for <c>POST /functions/{name}/runs</c> (operationId <c>startFunctionRun</c>,
/// beta).
/// Mirrors the spec schema <c>FunctionsStartFunctionRunRequestBody</c>.
/// </summary>
public sealed record StartFunctionRunRequest
{
    /// <summary>
    /// Parameter overrides for the Function execution. Can be an empty object but must be
    /// provided. Spec marks this REQUIRED.
    /// </summary>
    [JsonPropertyName("paramsOverride")]
    public required IReadOnlyDictionary<string, string> ParamsOverride { get; init; }
}

/// <summary>
/// Response body after successfully starting a Function run.
/// Returned by <c>POST /functions/{name}/runs</c> (operationId <c>startFunctionRun</c>, beta).
/// Mirrors the spec schema <c>StartFunctionRunResponseBodyResponseBody</c>.
/// </summary>
public sealed record FunctionRunStarted
{
    /// <summary>
    /// Unique identifier for this function execution. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; init; }
}

/// <summary>
/// Details of a specific Function run.
/// Returned by <c>GET /functions/{name}/runs/{correlationId}</c> (operationId
/// <c>getFunctionRun</c>, beta).
/// Mirrors the spec schema <c>GetFunctionRunDetailResponseBody</c>.
/// </summary>
public sealed record FunctionRun
{
    /// <summary>
    /// RFC 3339 timestamp when the Function run completed. Absent while the run is in
    /// progress.
    /// </summary>
    [JsonPropertyName("completedAtTime")]
    public DateTimeOffset? CompletedAtTime { get; init; }

    /// <summary>
    /// Context details of the Function run execution. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("context")]
    public FunctionRunContext? Context { get; init; }

    /// <summary>Unique identifier for this Function run. Spec marks this required on the response.</summary>
    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; init; }

    /// <summary>Name of the Function. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// RFC 3339 timestamp when the Function run started. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("startedAtTime")]
    public DateTimeOffset? StartedAtTime { get; init; }

    /// <summary>
    /// Execution status of the Function run. Valid values: `started`, `timeout`, `error`,
    /// `success`, `dropped`. One of: <c>started</c>, <c>timeout</c>, <c>error</c>,
    /// <c>success</c>, <c>dropped</c>. Spec marks this required on the response.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }
}

/// <summary>
/// Context details of the Function run execution.
/// Mirrors the spec schema <c>FunctionRunContextResponseBody</c>.
/// </summary>
public sealed record FunctionRunContext
{
    /// <summary>
    /// Unique request identifier for the execution. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; init; }

    /// <summary>The request payload sent to the Function.</summary>
    [JsonPropertyName("requestPayload")]
    public JsonElement? RequestPayload { get; init; }

    /// <summary>The response payload returned by the Function.</summary>
    [JsonPropertyName("responsePayload")]
    public JsonElement? ResponsePayload { get; init; }
}

/// <summary>
/// A single log entry from a Function execution.
/// One item of the <c>data</c> array returned by <c>GET /functions/{name}/logs</c> (operationId
/// <c>getFunctionLogs</c>, beta).
/// Mirrors the spec schema <c>FunctionLogEntryResponseBody</c>.
/// </summary>
public sealed record FunctionLogEntry
{
    /// <summary>The log message text. Spec marks this required on the response.</summary>
    [JsonPropertyName("log")]
    public string? Log { get; init; }

    /// <summary>
    /// RFC 3339 timestamp of when the log entry was produced. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("loggedAtTime")]
    public DateTimeOffset? LoggedAtTime { get; init; }
}

/// <summary>
/// A file stored in Functions storage.
/// One item of the <c>data</c> array returned by <c>GET /functions-storage/ls</c> (operationId
/// <c>listFunctionsStorageFiles</c>, beta).
/// Mirrors the spec schema <c>FunctionsStorageFileResponseBody</c>.
/// </summary>
public sealed record FunctionStorageFile
{
    /// <summary>
    /// RFC 3339 timestamp of when the file was last modified. Spec marks this required on
    /// the response.
    /// </summary>
    [JsonPropertyName("modifiedAtTime")]
    public DateTimeOffset? ModifiedAtTime { get; init; }

    /// <summary>The name of the file. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>The size of the file in bytes. Spec marks this required on the response.</summary>
    [JsonPropertyName("sizeBytes")]
    public long? SizeBytes { get; init; }

    /// <summary>
    /// Presigned URLs for this file. Only present when includeDownloadUrls or
    /// includeUploadUrls is set.
    /// </summary>
    [JsonPropertyName("urls")]
    public IReadOnlyList<FunctionStorageSignedUrl>? Urls { get; init; }
}

/// <summary>
/// A presigned URL for downloading or uploading a file in Functions storage.
/// Mirrors the spec schema <c>FunctionsStorageSignedUrlResponseBody</c>.
/// </summary>
public sealed record FunctionStorageSignedUrl
{
    /// <summary>
    /// RFC 3339 timestamp when the presigned URL expires. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public DateTimeOffset? ExpiresAtTime { get; init; }

    /// <summary>The presigned URL. Spec marks this required on the response.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// The type of presigned URL. Valid values: `download`, `upload`, `unknown`. One of:
    /// <c>download</c>, <c>upload</c>, <c>unknown</c>. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("urlType")]
    public string? UrlType { get; init; }
}

/// <summary>
/// A file in Functions storage with a presigned download URL.
/// Returned by <c>GET /functions-storage/files</c> (operationId <c>getFunctionStorageFile</c>,
/// beta).
/// Mirrors the spec schema <c>GetFunctionStorageFileDetailResponseBody</c>.
/// </summary>
public sealed record FunctionStorageFileDetail
{
    /// <summary>
    /// A presigned URL for a file in Functions storage. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("downloadGet")]
    public FunctionStorageSignedUrlOfType? DownloadGet { get; init; }

    /// <summary>The name of the file. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A presigned URL for a file in Functions storage.
/// Mirrors the spec schema <c>FunctionsStorageSignedUrlOfTypeResponseBody</c>.
/// </summary>
public sealed record FunctionStorageSignedUrlOfType
{
    /// <summary>
    /// RFC 3339 timestamp when the presigned URL expires. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("expiresAtTime")]
    public DateTimeOffset? ExpiresAtTime { get; init; }

    /// <summary>The presigned URL. Spec marks this required on the response.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }
}

/// <summary>
/// Request body for creating a new file in Functions storage.
/// Request body for <c>POST /functions-storage/files</c> (operationId
/// <c>createFunctionStorageFile</c>, beta).
/// Mirrors the spec schema <c>FunctionsStorageCreateFunctionStorageFileRequestBody</c>.
/// </summary>
public sealed record CreateFunctionStorageFileRequest
{
    /// <summary>The name of the file to create. Spec marks this REQUIRED.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

/// <summary>
/// A presigned upload URL for creating a new file in Functions storage.
/// Returned by <c>POST /functions-storage/files</c> (operationId
/// <c>createFunctionStorageFile</c>, beta).
/// Mirrors the spec schema <c>CreateFunctionStorageFileDetailResponseBody</c>.
/// </summary>
public sealed record FunctionStorageFileCreated
{
    /// <summary>The name of the file. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A presigned URL for a file in Functions storage. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("uploadPut")]
    public FunctionStorageSignedUrlOfType? UploadPut { get; init; }
}

/// <summary>
/// A presigned upload URL for overwriting an existing file in Functions storage.
/// Returned by <c>PUT /functions-storage/files</c> (operationId
/// <c>updateFunctionStorageFile</c>, beta).
/// Mirrors the spec schema <c>UpdateFunctionStorageFileDetailResponseBody</c>.
/// </summary>
public sealed record FunctionStorageFileUpdated
{
    /// <summary>The name of the file. Spec marks this required on the response.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A presigned URL for a file in Functions storage. Spec marks this required on the
    /// response.
    /// </summary>
    [JsonPropertyName("uploadPut")]
    public FunctionStorageSignedUrlOfType? UploadPut { get; init; }
}
