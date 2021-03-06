﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using Kontract.Attributes;
using Kontract.FileSystem.Nodes.Abstract;
using Kontract.Interfaces;
using Kontract.Interfaces.Common;
using Kontract.Interfaces.Text;

namespace plugin_test_adapters
{
    [Export(typeof(IPlugin))]
    [PluginExtensionInfo("*.text")]
    [PluginInfo("Test-Text-Id")]
    public class TestTextPlugin : ITextAdapter, ILoadFiles, IIdentifyFiles
    {
        private IEnumerable<TextEntry> _texts;

        public IEnumerable<TextEntry> Entries => _texts;

        public string NameFilter => ".*";

        public int NameMaxLength => 0;

        public string LineEndings { get; set; }

        public bool LeaveOpen { get; set; }

        public bool Identify(StreamInfo file, BaseReadOnlyDirectoryNode fileSystem)
        {
            using (var br = new BinaryReader(file.FileData, Encoding.ASCII, LeaveOpen))
            {
                return br.ReadUInt32() == 0x16161616;
            }
        }

        public void Load(StreamInfo file, BaseReadOnlyDirectoryNode fileSystem)
        {
            // Here a format class can get initialized and all opened files passed in
            var buffer = new byte[5];
            file.FileData.Position = 4;
            file.FileData.Read(buffer, 0, 5);
            _texts = new List<TextEntry> { new TextEntry { Name = "First entry", OriginalText = Encoding.ASCII.GetString(buffer) } };
        }

        public void Dispose() { }
    }
}
