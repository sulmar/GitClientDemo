using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //GitInit();

            CommitTest();

            // CommitTest2();

            LogTest();

        }

        private static void LogTest()
        {
            using (var repo = new Repository(@"files\folder1"))
            {
                var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

                foreach (Commit c in repo.Commits.Take(15))
                {
                    Console.WriteLine(string.Format("commit {0}", c.Id));

                    if (c.Parents.Count() > 1)
                    {
                        Console.WriteLine("Merge: {0}",
                            string.Join(" ", c.Parents.Select(p => p.Id.Sha.Substring(0, 7)).ToArray()));
                    }

                    Console.WriteLine(string.Format("Author: {0} <{1}>", c.Author.Name, c.Author.Email));
                    Console.WriteLine("Date:   {0}", c.Author.When.ToString(RFC2822Format, CultureInfo.InvariantCulture));
                    Console.WriteLine();
                    Console.WriteLine(c.Message);
                    Console.WriteLine();
                }
            }
        }

        private static void CommitTest2()
        {
            string filename = @"C:\Users\marci\Documents\WebAPI.docx";

            using (var repo = new Repository(@"files\folder1"))
            {
                // Write content to file system
                 //var content = "Commit this!";

                File.Copy(filename, Path.Combine(repo.Info.WorkingDirectory, "webapi.doc"));

                // File.WriteAllText(Path.Combine(repo.Info.WorkingDirectory, "fileToCommit.txt"), content);

                // Stage the file
                repo.Stage("webapi.doc");

                // Create the committer's signature and commit
                Signature author = new Signature("Marcin", "marcin.sulecki@gmail.com", DateTime.Now);
                Signature committer = author;

                // Commit to the repository
                Commit commit = repo.Commit("Web API the best!", author, committer);
            }
        }

        private static void CommitTest()
        {
            using (var repo = new Repository(@"files\folder1"))
            {
                // Write content to file system
                var content = "Commit this v2!";
                File.WriteAllText(Path.Combine(repo.Info.WorkingDirectory, "fileToCommit.txt"), content);

                // Stage the file
                repo.Stage("fileToCommit.txt");

                // Create the committer's signature and commit
                Signature author = new Signature("Marcin", "marcin.sulecki@gmail.com", DateTime.Now);
                Signature committer = author;

                // Commit to the repository
                Commit commit = repo.Commit("Here's a commit i made v2!", author, committer);
            }
        }

        /// <summary>
        /// https://github.com/libgit2/libgit2sharp/wiki/git-init
        /// </summary>
        private static void GitInit()
        {
            string rootedPath = Repository.Init(@"files\folder1");

            Console.WriteLine(rootedPath);
        }
    }
}
