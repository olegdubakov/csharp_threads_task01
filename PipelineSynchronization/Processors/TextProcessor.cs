﻿namespace PipelineSynchronization.Processors
{
    using System;
    using Interfaces;
    using Models;

    internal class TextProcessor : IProcessor<Site>
    {
        private readonly string _fileName;

        private readonly IFileWriter _writer;

        private readonly IParser _parser;

        public TextProcessor(
            IFileWriter writer,
            IConfiguration configuration, 
            IParser parser)
        {
            this._writer = writer;
            this._parser = parser;
            this._fileName = configuration.TextFileName;
        }
        
        public Site ProcessItem(Site site)
        {
            Console.WriteLine("Put TXT ({0})", site.Url);

            this._writer
                .Write(
                    this._parser.Parse(site.Html),
                    this._fileName);

            return site;
        }
    }
}
