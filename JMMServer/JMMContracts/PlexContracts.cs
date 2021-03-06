﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JMMContracts.PlexContracts
{
    [XmlType("MediaContainer")]
    [Serializable]
    public class MediaContainer
    {
        [XmlElement("Video")]
        public List<Video> Videos { get; set; }

        [XmlElement("Directory")]
        public List<Video> Directories { get; set; }

        [XmlAttribute("viewGroup")]
        public string ViewGroup { get; set; }

        [XmlAttribute("title1")]
        public string Title1 { get; set; }

        [XmlAttribute("art")]
        public string Art { get; set; }

        [XmlAttribute("title2")]
        public string Title2 { get; set; }

        [XmlAttribute("viewMode")]
        public string ViewMode { get; set; }

        [XmlAttribute("contenttype")]
        public string ContentType { get; set; }

        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlAttribute("identifier")]
        public string Identifier { get; set; }

        [XmlAttribute("mediaTagPrefix")]
        public string MediaTagPrefix { get; set; }

        [XmlAttribute("mediaTagVersion")]
        public string MediaTagVersion { get; set; }

        [XmlAttribute("allowSync")]
        public string AllowSync { get; set; }

        [XmlAttribute("totalSize")]
        public string TotalSize { get; set; }

        [XmlAttribute("nocache")]
        public string NoCache { get; set; }
        
        [XmlAttribute("offset")]
        public string Offset { get; set; }


    }
  
    [XmlType("Video")]
    [Serializable]
    public class Video
    {
        [XmlIgnore]
        public DateTime AirDate { get; set; }

        [XmlIgnore]
        public Contract_AnimeGroup Group { get; set; }

        [XmlElement("Media")]
        public List<Media> Medias { get; set; }

        [XmlElement("Role")]
        public List<Tag> Roles { get; set; }

        [XmlElement("Tag")]
        public List<Tag> Tags { get; set; }

        [XmlElement("Genre")]
        public List<Tag> Genres { get; set; }

        [XmlAttribute("art")]
        public string Art { get; set; }
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("thumb")]
        public string Thumb { get; set; }


        [XmlAttribute("ratingKey")]
        public string RatingKey { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("guid")]
        public string Guid { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("summary")]
        public string Summary { get; set; }

        [XmlAttribute("year")]
        public string Year { get; set; }

        [XmlAttribute("duration")]
        public string Duration { get; set; }

        [XmlAttribute("episode_count")]
        public string EpisodeCount { get; set; }

        [XmlAttribute("updatedAt")]
        public string UpdatedAt { get; set; }

        [XmlAttribute("addedAt")]
        public string AddedAt { get; set; }

        [XmlAttribute("originallyAvailableAt")]
        public string OriginallyAvailableAt { get; set; }

        [XmlAttribute("leafCount")]
        public string LeafCount { get; set; }

        [XmlAttribute("viewedLeafCount")]
        public string ViewedLeafCount { get; set; }

        [XmlAttribute("original_title")]
        public string OriginalTitle { get; set; }

        [XmlAttribute("source_title")]
        public string SourceTitle { get; set; }

        [XmlAttribute("rating")]
        public string Rating { get; set; }

        [XmlAttribute("season")]
        public string Season { get; set; }

        [XmlIgnore]
        public int EpNumber { get; set; }

        [XmlAttribute("viewCount")]
        public string ViewCount { get; set; }
        [XmlAttribute("viewOffset")]
        public string ViewOffset { get; set; }
    }

    [Serializable]
    public class Tag
    {
        [XmlAttribute("tag")]
        public string Value { get; set; }
        
    }
    [XmlType("Media")]
    [Serializable]
    public class Media
    {



        [XmlElement("Part")]
        public List<Part> Parts { get; set; }

        [XmlAttribute("duration")]
        public string Duration { get; set; }

        [XmlAttribute("videoFrameRate")]
        public string VideoFrameRate { get; set; }

        [XmlAttribute("container")]
        public string Container { get; set; }

        [XmlAttribute("videoCodec")]
        public string VideoCodec { get; set; }

        [XmlAttribute("audioCodec")]
        public string AudioCodec { get; set; }

        [XmlAttribute("audioChannels")]
        public string AudioChannels { get; set; }

        [XmlAttribute("aspectRatio")]
        public string AspectRatio { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }

        [XmlAttribute("width")]
        public string Width { get; set; }


        [XmlAttribute("bitrate")]
        public string Bitrate { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("videoResolution")]
        public string VideoResolution { get; set; }

        [XmlAttribute("optimizedForStreaming")]
        public string OptimizedForStreaming { get; set; }
    }
    [XmlType("Part")]
    public class Part
    {
        [XmlAttribute("accessible")]
        public string Accessible { get; set; }

        [XmlAttribute("exists")]
        public string Exists { get; set; }
        
        [XmlElement("Stream")]
        public List<Stream> Streams { get; set; }

        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlAttribute("duration")]
        public string Duration { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("container")]
        public string Container { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("file")]
        public string File { get; set; }

        [XmlAttribute("optimizedForStreaming")]
        public string OptimizedForStreaming { get; set; }


        [XmlAttribute("has64bitOffsets")]
        public string Has64bitOffsets { get; set; }
    }

    [XmlType("Stream")]
    public class Stream
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("language")]
        public string Language { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }


        [XmlAttribute("duration")]
        public string Duration { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }

        [XmlAttribute("width")]
        public string Width { get; set; }

        [XmlAttribute("bitrate")]
        public string Bitrate { get; set; }

        [XmlAttribute("subIndex")]
        public string SubIndex { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("scanType")]
        public string ScanType { get; set; }
        [XmlAttribute("refFrames")]
        public string RefFrames { get; set; }
        [XmlAttribute("profile")]
        public string Profile { get; set; }

        [XmlAttribute("level")]
        public string Level { get; set; }

        [XmlAttribute("headerStripping")]
        public string HeaderStripping { get; set; }

        [XmlAttribute("hasScalingMatrix")]
        public string HasScalingMatrix { get; set; }

        [XmlAttribute("frameRateMode")]
        public string FrameRateMode { get; set; }
        
        [XmlAttribute("file")]
        public string File { get; set; }

        [XmlAttribute("frameRate")]
        public string FrameRate { get; set; }

        [XmlAttribute("colorSpace")]
        public string ColorSpace { get; set; }

        [XmlAttribute("codecID")]
        public string CodecID { get; set; }


        [XmlAttribute("chromaSubsampling")]
        public string ChromaSubsampling { get; set; }

        [XmlAttribute("cabac")]
        public string Cabac { get; set; }


        [XmlAttribute("bitDepth")]
        public string BitDepth { get; set; }

        [XmlAttribute("index")]
        public string Index { get; set; }

        [XmlIgnore]
        public int idx;

        [XmlAttribute("codec")]
        public string Codec { get; set; }

        [XmlAttribute("streamType")]
        public string StreamType { get; set; }

        [XmlAttribute("orientation")]
        public string Orientation { get; set; }

        [XmlAttribute("qpel")]
        public string QPel { get; set; }

        [XmlAttribute("gmc")]
        public string GMC { get; set; }

        [XmlAttribute("bvop")]
        public string BVOP { get; set; }

        [XmlAttribute("samplingRate")]
        public string SamplingRate { get; set; }
        [XmlAttribute("languageCode")]
        public string LanguageCode { get; set; }

        [XmlAttribute("channels")]
        public string Channels { get; set; }


        [XmlAttribute("selected")]
        public string Selected { get; set; }

        [XmlAttribute("dialogNorm")]
        public string DialogNorm { get; set; }


        [XmlAttribute("bitrateMode")]
        public string BitrateMode { get; set; }


        [XmlAttribute("format")]
        public string Format { get; set; }
        [XmlAttribute("default")]
        public string Default { get; set; }

        [XmlAttribute("forced")]
        public string Forced { get; set; }

        [XmlAttribute("pixelAspectRatio")]
        public string PixelAspectRatio { get; set; }

        [XmlIgnore]
        public float PA { get; set; }
    }

    [XmlType("User")]
    public class PlexContract_User
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }
    }

    [XmlType("Users")]
    public class PlexContract_Users
    {
        [XmlElement("User")]
        public List<PlexContract_User> Users { get; set; }
    }
    public enum JMMType
    {
        GroupFilter,
        GroupUnsort,
        Group,
        Serie,
        EpisodeType,
        Episode,
        Movie,
        File
    }
}
