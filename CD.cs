using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Practicum4
{
    class CD
    {
        public string title { get; set; }
        public string artist { get; set; }
        public List<Track> tracks { get; set; }

        public XDocument generateXML() {
            var CDXml = new XDocument();
            var rootElement = new XElement("CD",
                new XAttribute("title", this.title),
                 new XAttribute("artist", this.artist),
                 new XElement("tracks",
                this.tracks.Select(track =>
                new XElement("track",
                    new XElement("title", track.title),
                    new XElement("artist", track.artist),
                    new XElement("length", track.length.ToString())
                    )))
                );
            CDXml.Add(rootElement);
            return CDXml;
        } 

    }
}
