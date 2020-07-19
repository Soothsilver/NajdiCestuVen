using System;
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
            if (!ProcessRunner.RunProcess("git", "tag -a v" + versionNumber + "-m v" + versionNumber))
            {
                return;
            }
            
            // create new release
            await UploadToGitHub(versionNumber);

            Console.WriteLine("INFORMATION: Don't forget to git push!");
        }

        public static async Task UploadToGitHub(string version)
        {
            var client = new GitHubClient(new ProductHeaderValue("Soothsilver/NajdiCestuVen"));
            client.Credentials = new Credentials(Environment.GetEnvironmentVariable("GITHUB_API_TOKEN"));
            Console.WriteLine("Hello: " + (await client.User.Current()).Email);
        }
    }
}