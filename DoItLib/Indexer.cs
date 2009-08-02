using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DoItLib
{
    public class Indexer
    {
        private Stack<string> _folderStack;
        private List<String> _shortcuts;

        public Indexer(params string[] initialFolders)
        {
            _folderStack = new Stack<string>();
            _shortcuts = new List<string>();
            foreach (string item in initialFolders)
            {
                _folderStack.Push(item);
            }
        }

        public List<String> Index()
        {
            while (_folderStack.Count > 0)
            {
                ProcessFolder(_folderStack.Pop());
            }
            return _shortcuts;
        }

        private void ProcessFolder(string folder)
        {
            try
            {
                foreach (string item in Directory.GetDirectories(folder))
                {
                    _folderStack.Push(item);
                }
                foreach (string item in Directory.GetFiles(folder))
                {
                    if (item.EndsWith(".lnk"))
                    {
                        _shortcuts.Add(item);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unable to access folder {0}", folder);
            }
        }
    }
}
