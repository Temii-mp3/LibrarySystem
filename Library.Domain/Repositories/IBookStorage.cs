public interface IBookStorage
{
    Task<StoredFileInfo> SaveAsync(Stream content, string extension, CancellationToken ct = default);
    Task<Stream> OpenReadAsync(string storageKey, CancellationToken ct = default);
    Task DeleteAsync(string storageKey, CancellationToken ct = default);
}

public record StoredFileInfo(string StorageKey, long SizeBytes, string ContentHash);