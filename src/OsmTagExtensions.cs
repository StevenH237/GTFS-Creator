using OsmSharp.Tags;

namespace Nixill.GTFS
{
  public static class OsmTagExtensions
  {
    public static string Try(this TagsCollectionBase tags, string tagName)
    {
      if (tags.ContainsKey(tagName)) return tags[tagName];
      else return null;
    }
  }
}