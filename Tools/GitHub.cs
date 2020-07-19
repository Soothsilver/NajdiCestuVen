using System;
using System.IO;
using System.Threading.Tasks;
using Octokit;

namespace Tools
{
    internal class GitHub
    {
        public static async Task CompleteCreateNewRelease(string versionNumber)
        {
            // git commit and tag
            if (!ProcessRunner.RunProcess("git", "commit -a -m \"Commit build " + versionNumber + "."))
            {
                return;
            }
            if (!ProcessRunner.RunProcess("git", "tag -a v" + versionNumber + " -m v" + versionNumber))
            {
                return;
            }
            if (!ProcessRunner.RunProcess("git", "push --all --follow-tags"))
            {
                return;
            }
            
            // create new release
            if (!await UploadToGitHub(versionNumber))
            {
                return;
            }
        }

        public static async Task<bool> UploadToGitHub(string version)
        {
            // Create API client
            var client = new GitHubClient(new ProductHeaderValue("Soothsilver.NajdiCestuVen"));
            string? apiToken = Environment.GetEnvironmentVariable("GITHUB_API_TOKEN");
            if (string.IsNullOrEmpty(apiToken))
            {
                Console.WriteLine("Error. Set the environment variable GITHUB_API_TOKEN.");
                return false;
            }
            client.Credentials = new Credentials(apiToken);
            
            // Create release
            Console.WriteLine("- Creating GitHub release as draft...");
            var release = new NewRelease("v" + version);
            release.Name = "Version " + version;
            release.Draft = true;
            release.Body =
                "This release contains the Android .apk, the Windows installer and a .zip file with the Windows installation taken from the current stage in development.";
            var result = await client.Repository.Release.Create("Soothsilver", "NajdiCestuVen", release);
            Console.WriteLine("- Created GitHub release " + result.Id + " as a DRAFT.");
            
            // Upload assets
            await UploadAsset("EscapeTheStorms-" + version + ".zip", "application/zip", version, client);
            // await UploadAsset("EscapeTheStormsSetup-" + version + ".exe", "application/vnd.microsoft.portable-executable", version, client);
            // await UploadAsset("NajdiCestuVen-Signed-" + version + ".apk", "application/vnd.android.package-archive", version, client);
            // application/vnd.microsoft.portable-executable
            // application/vnd.android.package-archive
            return true;
        }

        private static async Task UploadAsset(string filename, string mimeType, string version, GitHubClient client)
        {
            Console.WriteLine("- Uploading asset " + filename + "...");
            string fullFileName = Path.Combine("Build\\Output\\" + version + "\\" + filename);
            await using (var contents = File.OpenRead(fullFileName))
            { 
                var assetUpload = new ReleaseAssetUpload(filename, mimeType, contents, TimeSpan.FromMinutes(20));
                var release = await client.Repository.Release.Get("Soothsilver", "NajdiCestuVen", "v" + version);
                var result = await client.Repository.Release.UploadAsset(release, assetUpload);
                Console.WriteLine("- Uploaded asset at " + result.BrowserDownloadUrl);
            }
        }
    }
}