using System.Text.Json;

namespace ConsoleApp07.Applications;

public class HttpPhotosClient
{
    public static async Task Execute()
    {
        // Get the photos and display their titles and URLs
        //await GetPhotosRequest();

        // Get the photos and albums, then display the albums with their associated photos
        await GetPhotosAlbums();
    }

    private static async Task GetPhotosRequest()
    {
        var url = "https://jsonplaceholder.typicode.com/photos";
        var client = new HttpClient();

        try
        {
            var response = await client.GetStringAsync(url);
            var photos = JsonSerializer.Deserialize<List<Photo>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (photos is not null)
            {
                photos.ForEach(photo => Console.WriteLine("Name: {0} , Url: {1}", photo.Title, photo.Url));
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            throw;
        }
    }

    private static async Task GetPhotosAlbums()
    {
        var client = new HttpClient();
        var jsonPhotos = await client.GetStringAsync("https://jsonplaceholder.typicode.com/photos");
        var jsonAlbums = await client.GetStringAsync("https://jsonplaceholder.typicode.com/albums");

        var photos = JsonSerializer.Deserialize<List<Photo>>(jsonPhotos, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var albums = JsonSerializer.Deserialize<List<Album>>(jsonAlbums, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        try
        {
            if (photos != null && albums != null)
            {
                var photosAlbums = albums
                    .Join(
                        photos,
                        a => a.Id,
                        p => p.AlbumId,
                        (album, photo) => new
                        {
                            album.Id,
                            album.Title,
                            PhotoName = photo.Title,
                            PhotoUrl = photo.Url
                        }
                    )
                    .GroupBy(g => g.Id)
                    .Select(g => new
                    {
                        g.Key,
                        g.First().Title,
                        Photos = g.ToList()
                    })
                    .Take(2)
                    .ToList();

                foreach (var photoAlbum in photosAlbums)
                {
                    Console.WriteLine("----- Album: {0} -----", photoAlbum.Title);

                    Console.WriteLine("--------------- Photos --------------");
                    foreach (var photo in photoAlbum.Photos)
                    {

                        Console.WriteLine("Name: {0}, Url:{1}", photo.PhotoName, photo.PhotoUrl);
                    }

                    Console.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {

            Console.Error.WriteLine(ex.Message);
        }
    }

    private class Photo
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string? Title { get; set; } = default!;
        public string? Url { get; set; }
        public string? ThumbnailUrl { get; set; }
    }

    private class Album
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
    }
}
