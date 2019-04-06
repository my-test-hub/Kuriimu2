﻿using Kontract.Interfaces.Common;
using Kontract.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kontract.Interfaces.Archive;
using Komponent.IO;

namespace plugin_yuusha_shisu.PAC
{
    [Export(typeof(PacAdapter))]
    [Export(typeof(IPlugin))]
    [PluginInfo("plugin_yuusha_shisu","HeroWillDie","PAC","StorMyu")]
    [PluginExtensionInfo("*.pac")]
    public sealed class PacAdapter : IPlugin,IArchiveAdapter,IIdentifyFiles,ILoadFiles,ISaveFiles,IArchiveReplaceFiles
    {
        private PAC _format;

        #region Properties
        public List<ArchiveFileInfo> Files => _format?.Files ;
        public bool FileHasExtendedProperties => false;
        public bool LeaveOpen { get; set; }

        #endregion

        public bool Identify(StreamInfo fileInfo)
        {
            try
            {
                return new BinaryReaderX(fileInfo.FileData, LeaveOpen).ReadString(4) == "ARC\0";
            }
            catch (Exception)
            {
                return false;
            }
        }

        void ILoadFiles.Load(StreamInfo fileInfo)
        {
            _format = new PAC(fileInfo.FileData);
        }

        public void Save(StreamInfo primaryFile, int versionIndex = 0)
        {
            _format.Save(primaryFile.FileData);
        }

        void IDisposable.Dispose()
        {
            _format = null;
        }

    }
}