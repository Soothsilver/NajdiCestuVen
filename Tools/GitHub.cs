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
            Console.WriteLine("- Creating GitHub release...");
            var release = new NewRelease("v" + version);
            release.Name = "Version " + version;
            release.Draft = false;
            release.Body =
                "This release contains the Android .apk, the Windows installer and a .zip file with the Windows installation taken from the current stage in development.";

            try
            {
                var result = await client.Repository.Release.Create("Soothsilver", "NajdiCestuVen", release);
                Console.WriteLine("- Created GitHub release " + result.Id + " as a DRAFT.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("- Creating a release failed because: " + ex.Message + " but continuing. Perhaps it already exists.");
            }

            // Upload assets
            Console.WriteLine("- Uploading binary assets now. This may take some time.");
            if (!UploadAsset("EscapeTheStorms-" + version + ".zip", version, client))
            {
                return false;
            }
            if (!UploadAsset("EscapeTheStormsSetup-" + version + ".exe", version, client))
            {
                return false;
            }
            if (!UploadAsset("NajdiCestuVen-Signed-" + version + ".apk", version, client))
            {
                return false;
            }
            return true;
        }

        private static bool UploadAsset(string filename, string version, GitHubClient client)
        {
            Console.WriteLine("- Uploading asset " + filename + "...");
            // filename = "example.txt";
            string fullFileName = Path.Combine("Build\\Output\\" + version + "\\" + filename);

            if (!ProcessRunner.RunProcess("Build\\ThirdParty\\github-release.exe",
                "upload --user Soothsilver --repo NajdiCestuVen -s " + Environment.GetEnvironmentVariable("GITHUB_API_TOKEN") + " --tag v" + version + " --name " +
                filename + " --file " + fullFileName))
            {
                return false;
            }

            return true;
        }
    }
}