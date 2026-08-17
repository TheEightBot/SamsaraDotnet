namespace Samsara.Sdk.Models.Compliance;

using System.Text.Json.Serialization;

/// <summary>
/// A driver's tachograph activity history entry, returned by
/// <c>GET /fleet/drivers/tachograph-activity/history</c>.
/// </summary>
public sealed record TachographActivity
{
    /// <summary>The driver's tachograph activities over the requested window.</summary>
    [JsonPropertyName("activity")]
    public IReadOnlyList<TachographActivityEntry>? Activity { get; init; }

    /// <summary>The driver the activities belong to.</summary>
    [JsonPropertyName("driver")]
    public TachographDriver? Driver { get; init; }
}

/// <summary>
/// A single tachograph activity interval. Mirrors the spec's
/// <c>TachographActivity</c> array-item schema.
/// </summary>
public sealed record TachographActivityEntry
{
    /// <summary>Start time of the activity, in RFC 3339 format.</summary>
    [JsonPropertyName("startTime")]
    public string? StartTime { get; init; }

    /// <summary>End time of the activity, in RFC 3339 format.</summary>
    [JsonPropertyName("endTime")]
    public string? EndTime { get; init; }

    /// <summary>The activity state (e.g. <c>driving</c>, <c>work</c>, <c>rest</c>, <c>available</c>).</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Whether the activity was entered manually by the driver.</summary>
    [JsonPropertyName("isManualEntry")]
    public bool? IsManualEntry { get; init; }
}

/// <summary>
/// A tachograph file-history entry, returned by
/// <c>GET /fleet/drivers/tachograph-files/history</c> (which carries a
/// <see cref="Driver"/>) and <c>GET /fleet/vehicles/tachograph-files/history</c>
/// (which carries a <see cref="Vehicle"/>).
/// </summary>
public sealed record TachographFile
{
    /// <summary>The driver the files belong to (driver-files endpoint).</summary>
    [JsonPropertyName("driver")]
    public TachographDriver? Driver { get; init; }

    /// <summary>The vehicle the files belong to (vehicle-files endpoint).</summary>
    [JsonPropertyName("vehicle")]
    public TachographVehicle? Vehicle { get; init; }

    /// <summary>The tachograph files over the requested window.</summary>
    [JsonPropertyName("files")]
    public IReadOnlyList<TachographFileEntry>? Files { get; init; }
}

/// <summary>
/// A single tachograph file. Mirrors the spec's <c>TachographDriverFile</c>
/// (driver-files endpoint) and <c>TachographVehicleFile</c> (vehicle-files
/// endpoint); the union of their fields is exposed here.
/// </summary>
public sealed record TachographFileEntry
{
    /// <summary>Unique identifier of the file.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Signed download URL for the file.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Time the file was created, in RFC 3339 format.</summary>
    [JsonPropertyName("createdAtTime")]
    public string? CreatedAtTime { get; init; }

    /// <summary>The driver card number (driver-files endpoint only).</summary>
    [JsonPropertyName("cardNumber")]
    public string? CardNumber { get; init; }

    /// <summary>The vehicle identification number (vehicle-files endpoint only).</summary>
    [JsonPropertyName("vehicleIdentificationNumber")]
    public string? VehicleIdentificationNumber { get; init; }
}

/// <summary>
/// A minified driver reference on a tachograph response. Mirrors the spec's
/// <c>driverTinyResponse</c>.
/// </summary>
public sealed record TachographDriver
{
    /// <summary>Samsara ID of the driver.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the driver.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}

/// <summary>
/// A minified vehicle reference on a tachograph response. Mirrors the spec's
/// <c>vehicleTinyResponse</c>.
/// </summary>
public sealed record TachographVehicle
{
    /// <summary>Samsara ID of the vehicle.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Name of the vehicle.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>External identifiers for the vehicle.</summary>
    [JsonPropertyName("externalIds")]
    public IReadOnlyDictionary<string, string>? ExternalIds { get; init; }
}

/// <summary>
/// Request to create a tachograph file upload
/// (<c>POST /fleet/tachograph/file-uploads</c>). Mirrors the spec's
/// <c>TachographFileUploadsPostTachographFileUploadRequestBody</c>.
/// </summary>
public sealed record CreateTachographFileUploadRequest
{
    /// <summary>Base64-encoded MD5 digest of the file being uploaded.</summary>
    [JsonPropertyName("contentMd5")]
    public required string ContentMd5 { get; init; }

    /// <summary>MIME type of the upload. The spec permits only <c>application/octet-stream</c>.</summary>
    [JsonPropertyName("contentType")]
    public required string ContentType { get; init; }

    /// <summary>Size of the file in bytes.</summary>
    [JsonPropertyName("fileSizeBytes")]
    public required long FileSizeBytes { get; init; }

    /// <summary>Kind of tachograph file: <c>driverCard</c> or <c>vehicleUnit</c>.</summary>
    [JsonPropertyName("fileType")]
    public required string FileType { get; init; }
}

/// <summary>
/// Pre-signed upload target returned by <c>POST /fleet/tachograph/file-uploads</c>.
/// Mirrors the spec's <c>TachographFileUploadResponseBody</c>. Send the file to
/// <see cref="UploadUrl"/> with every header in <see cref="RequiredHeaders"/>
/// before <see cref="ExpiresAtTime"/>.
/// </summary>
public sealed record TachographFileUpload
{
    /// <summary>UTC instant after which <see cref="UploadUrl"/> stops working.</summary>
    [JsonPropertyName("expiresAtTime")]
    public DateTimeOffset? ExpiresAtTime { get; init; }

    /// <summary>Headers that must accompany the upload request.</summary>
    [JsonPropertyName("requiredHeaders")]
    public IReadOnlyList<TachographUploadRequiredHeader>? RequiredHeaders { get; init; }

    /// <summary>Pre-signed URL to upload the file to.</summary>
    [JsonPropertyName("uploadUrl")]
    public string? UploadUrl { get; init; }
}

/// <summary>
/// A single header required on a tachograph file upload. Mirrors the spec's
/// <c>TachographUploadRequiredHeaderResponseBody</c>.
/// </summary>
public sealed record TachographUploadRequiredHeader
{
    /// <summary>Header name.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Header value.</summary>
    [JsonPropertyName("value")]
    public string? Value { get; init; }
}

/// <summary>
/// TachographLiveData object.
/// One item of the <c>data</c> array returned by <c>GET /fleet/tachograph-live-data/latest</c>
/// (operationId <c>listTachographLiveData</c>, beta).
/// Mirrors the spec schema <c>EntityListTachographLiveDataTypeResponseBody</c>.
/// </summary>
public sealed record TachographLiveData
{
    /// <summary>
    /// The driver's cumulated driving time across the previous and current week, as defined
    /// by ISO 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("cumulatedDrivingTimePreviousAndCurrentWeekMinute")]
    public long? CumulatedDrivingTimePreviousAndCurrentWeekMinute { get; init; }

    /// <summary>
    /// The driver's cumulative break time, as defined by ISO 16844-7 (Tachograph - Digital
    /// data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("cumulativeBreakTimeMinute")]
    public long? CumulativeBreakTimeMinute { get; init; }

    /// <summary>
    /// The driver's current daily driving time, as defined by ISO 16844-7 (Tachograph -
    /// Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("currentDailyDrivingTimeMinute")]
    public long? CurrentDailyDrivingTimeMinute { get; init; }

    /// <summary>
    /// The duration of the driver's currently selected activity, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("currentDurationOfSelectedActivityMinute")]
    public long? CurrentDurationOfSelectedActivityMinute { get; init; }

    /// <summary>
    /// The driver's current weekly driving time, as defined by ISO 16844-7 (Tachograph -
    /// Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("currentWeeklyDrivingTimeMinute")]
    public long? CurrentWeeklyDrivingTimeMinute { get; init; }

    /// <summary>Samsara ID for the driver.</summary>
    [JsonPropertyName("driverId")]
    public string? DriverId { get; init; }

    /// <summary>
    /// The expected duration of the driver's next break or rest period, as defined by ISO
    /// 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("durationOfNextBreakRestMinute")]
    public long? DurationOfNextBreakRestMinute { get; init; }

    /// <summary>
    /// The expected duration of the driver's next driving period, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("durationOfNextDrivingPeriodMinute")]
    public long? DurationOfNextDrivingPeriodMinute { get; init; }

    /// <summary>
    /// The timestamp marking the end of the driver's last daily rest period, as defined by
    /// ISO 16844-7 (Tachograph - Digital data interface).
    /// </summary>
    [JsonPropertyName("endOfLastDailyRestPeriod")]
    public string? EndOfLastDailyRestPeriod { get; init; }

    /// <summary>
    /// The timestamp marking the end of the driver's last weekly rest period, as defined by
    /// ISO 16844-7 (Tachograph - Digital data interface).
    /// </summary>
    [JsonPropertyName("endOfLastWeeklyRestPeriod")]
    public string? EndOfLastWeeklyRestPeriod { get; init; }

    /// <summary>The timestamp when the tachograph reading was measured.</summary>
    [JsonPropertyName("happenedAtTime")]
    public string? HappenedAtTime { get; init; }

    /// <summary>
    /// The maximum allowed daily driving time for the driver, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("maximumDailyDrivingTimeMinute")]
    public long? MaximumDailyDrivingTimeMinute { get; init; }

    /// <summary>
    /// The minimum required daily rest duration for the driver, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("minimumDailyRestMinute")]
    public long? MinimumDailyRestMinute { get; init; }

    /// <summary>
    /// The minimum required weekly rest duration for the driver, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("minimumWeeklyRestMinute")]
    public long? MinimumWeeklyRestMinute { get; init; }

    /// <summary>
    /// The number of times the driver has exceeded the 9-hour daily driving time limit, as
    /// defined by ISO 16844-7 (Tachograph - Digital data interface).
    /// </summary>
    [JsonPropertyName("numberOfTimes9hDailyDrivingTimesExceeded")]
    public long? NumberOfTimes9hDailyDrivingTimesExceeded { get; init; }

    /// <summary>
    /// The number of reduced daily rest periods the driver has used, as defined by ISO
    /// 16844-7 (Tachograph - Digital data interface).
    /// </summary>
    [JsonPropertyName("numberOfUsedReducedDailyRestPeriods")]
    public long? NumberOfUsedReducedDailyRestPeriods { get; init; }

    /// <summary>
    /// Open rest compensation owed from the second week before last, as defined by ISO
    /// 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("openCompensationInSecondWeekBeforeLastMinute")]
    public long? OpenCompensationInSecondWeekBeforeLastMinute { get; init; }

    /// <summary>
    /// Open rest compensation owed from the last week, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("openCompensationInTheLastWeekMinute")]
    public long? OpenCompensationInTheLastWeekMinute { get; init; }

    /// <summary>
    /// Open rest compensation owed from the week before last, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("openCompensationInWeekBeforeLastMinute")]
    public long? OpenCompensationInWeekBeforeLastMinute { get; init; }

    /// <summary>
    /// The driver's remaining driving time across the current two-week period, as defined
    /// by ISO 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("remaining2WeeksDrivingTimeMinute")]
    public long? Remaining2WeeksDrivingTimeMinute { get; init; }

    /// <summary>
    /// The driver's remaining current continuous driving time before a break is required,
    /// as defined by ISO 16844-7 (Tachograph - Digital data interface). Measured in
    /// minutes.
    /// </summary>
    [JsonPropertyName("remainingCurrentDrivingTimeMinute")]
    public long? RemainingCurrentDrivingTimeMinute { get; init; }

    /// <summary>
    /// The driver's remaining driving time for the current week, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("remainingDrivingTimeOfCurrentWeekMinute")]
    public long? RemainingDrivingTimeOfCurrentWeekMinute { get; init; }

    /// <summary>
    /// The driver's remaining driving time on the current shift, as defined by ISO 16844-7
    /// (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("remainingDrivingTimeOnCurrentShiftMinute")]
    public long? RemainingDrivingTimeOnCurrentShiftMinute { get; init; }

    /// <summary>
    /// Time remaining in the driver's current break or rest period, as defined by ISO
    /// 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("remainingTimeOfCurrentBreakRestMinute")]
    public long? RemainingTimeOfCurrentBreakRestMinute { get; init; }

    /// <summary>
    /// Time remaining until the driver's next required break or rest period, as defined by
    /// ISO 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("remainingTimeUntilNextBreakOrRestMinute")]
    public long? RemainingTimeUntilNextBreakOrRestMinute { get; init; }

    /// <summary>The tachograph card number for the driver.</summary>
    [JsonPropertyName("tachographCardNumber")]
    public string? TachographCardNumber { get; init; }

    /// <summary>
    /// Time remaining until the driver must begin a new daily rest period, as defined by
    /// ISO 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("timeLeftUntilNewDailyRestPeriodMinute")]
    public long? TimeLeftUntilNewDailyRestPeriodMinute { get; init; }

    /// <summary>
    /// Time remaining until the driver must begin a new weekly rest period, as defined by
    /// ISO 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("timeLeftUntilNewWeeklyRestPeriodMinute")]
    public long? TimeLeftUntilNewWeeklyRestPeriodMinute { get; init; }

    /// <summary>
    /// Time remaining until the driver's next driving period can begin, as defined by ISO
    /// 16844-7 (Tachograph - Digital data interface). Measured in minutes.
    /// </summary>
    [JsonPropertyName("timeLeftUntilNextDrivingPeriodMinute")]
    public long? TimeLeftUntilNextDrivingPeriodMinute { get; init; }

    /// <summary>Samsara ID for the vehicle associated with the live tachograph data.</summary>
    [JsonPropertyName("vehicleId")]
    public string? VehicleId { get; init; }

    /// <summary>
    /// The current working state of the driver (e.g. Rest, Availability, Work, Driving), as
    /// defined by ISO 16844-7 (Tachograph - Digital data interface).
    /// </summary>
    [JsonPropertyName("workingState")]
    public string? WorkingState { get; init; }
}
