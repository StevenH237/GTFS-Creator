using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OsmSharp;
using OsmSharp.Streams;

// GTFS Creator: From an OSM-XML output of a transit network and a schedule CSV, create most of a valid GTFS file.
namespace Nixill.GTFS
{
  public class GTFSCreatorMain
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the GTFS creator");

      // Select the OSM file
      Console.WriteLine("Select the OSM file containing route information (leave blank to quit): ");
#if DEBUG
      string file = @"data/testMap.osm";
#else
      string file = Console.ReadLine();
#endif
      if (file == null || file == "") return;

      // Start parsing the file
      using var stream = File.OpenRead(file);
      using var source = new XmlOsmStreamSource(stream);

      // For now let's just get all the components. I can optimize this later.
      Dictionary<long, Node> nodes = source.Where(x => x.Type == OsmGeoType.Node).ToDictionary(x => x.Id.Value, x => (Node)x);
      Dictionary<long, Way> ways = source.Where(x => x.Type == OsmGeoType.Way).ToDictionary(x => x.Id.Value, x => (Way)x);
      Dictionary<long, Relation> relations = source.Where(x => x.Type == OsmGeoType.Relation).ToDictionary(x => x.Id.Value, x => (Relation)x);

      // Let's start with route shapes
      var routes = relations.Where(x =>
      {
        var tags = x.Value.Tags;
        return tags.Try("type") == "route_master" &&
        (
          tags.Try("route_master") == "aerialway" ||
          tags.Try("route_master") == "bus" ||
          tags.Try("route_master") == "ferry" ||
          tags.Try("route_master") == "funicular" ||
          tags.Try("route_master") == "light_rail" ||
          tags.Try("route_master") == "subway" ||
          tags.Try("route_master") == "train" ||
          tags.Try("route_master") == "tram"
        );
      }).Select(x => x.Value);

      // Iterate the routes and make their shapes
      Dictionary<long, Route> routeShapes = new Dictionary<long, Route>();


    }
  }
}