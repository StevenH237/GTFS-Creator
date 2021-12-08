using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Streams;

// GTFS Creator: From an OSM-XML output of a transit network and a schedule CSV, 
namespace Nixill.GTFS
{
  public class GTFSCreatorMain
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the GTFS creator");

      // Select the OSM file
      Console.WriteLine("Select the OSM file containing route information (leave blank to quit): ");
      string file = Console.ReadLine();
      if (file == null || file == "") return;

      // Start parsing the file
      using var stream = File.OpenRead(file);
      using var source = new XmlOsmStreamSource(stream);

      // For now let's just get all the components. I can optimize this later.
      Dictionary<long, Node> nodes = source.Where(x => x.Type == OsmGeoType.Node).ToDictionary(x => x.Id.Value, x => (Node)x);
    }
  }
}