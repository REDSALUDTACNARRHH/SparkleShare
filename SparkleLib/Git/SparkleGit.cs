//   SparkleShare, a collaboration and sharing tool.
//   Copyright (C) 2010  Hylke Bons <hylkebons@gmail.com>
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU Lesser General Public License as 
//   published by the Free Software Foundation, either version 3 of the 
//   License, or (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.IO;
using System.Diagnostics;

using SparkleLib;

namespace SparkleLib.Git {

    public class SparkleGit : SparkleProcess {

        public static string ExecPath;
        public static string GitPath;
        public static string SSHPath;


        public SparkleGit (string path, string args) : base (path, args)
        {
            if (string.IsNullOrEmpty (GitPath))
                GitPath = LocateCommand ("git");

            StartInfo.FileName         = GitPath;
            StartInfo.WorkingDirectory = path;

            if (StartInfo.EnvironmentVariables.ContainsKey ("LANG"))
                StartInfo.EnvironmentVariables ["LANG"] = "en_US";
            else
                StartInfo.EnvironmentVariables.Add ("LANG", "en_US");

            if (StartInfo.EnvironmentVariables.ContainsKey ("GIT_TERMINAL_PROMPT"))
                StartInfo.EnvironmentVariables ["GIT_TERMINAL_PROMPT"] = "0";
            else
                StartInfo.EnvironmentVariables.Add ("GIT_TERMINAL_PROMPT", "0");

            if (!string.IsNullOrEmpty (SSHPath)) {
                if (StartInfo.EnvironmentVariables.ContainsKey ("GIT_SSH_COMMAND"))
                    StartInfo.EnvironmentVariables ["GIT_SSH_COMMAND"] = SSHPath;
                else
                    StartInfo.EnvironmentVariables.Add ("GIT_SSH_COMMAND", SSHPath);
            }

            if (string.IsNullOrEmpty (ExecPath))
                StartInfo.Arguments = args;
            else
                StartInfo.Arguments = "--exec-path=\"" + ExecPath + "\" " + args;
        }
    }


    public class SparkleGitBin : SparkleProcess {

        public static string GitBinPath;


        public SparkleGitBin (string path, string args) : base (path, args)
        {
            if (string.IsNullOrEmpty (GitBinPath))
                GitBinPath = LocateCommand ("git-bin");

            EnableRaisingEvents              = true;
            StartInfo.FileName               = GitBinPath;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.UseShellExecute        = false;
            StartInfo.WorkingDirectory       = path;
            StartInfo.CreateNoWindow         = true;
            StartInfo.Arguments              = args;
        }
    }
}
