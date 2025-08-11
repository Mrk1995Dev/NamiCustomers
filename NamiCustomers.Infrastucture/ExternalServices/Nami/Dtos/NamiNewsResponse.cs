using NamiCustomers.Infrastucture.Utilities;

namespace NamiCustomers.Infrastucture.ExternalServices.Nami.Dtos;

public class NamiNewsResponse
{
    public int id { get; set; }
    public DateTime date { get; set; }
    public string PersianDate => PersianDateUtility.GetPersianDateString(this.date);
    public DateTime date_gmt { get; set; }
    public Guid guid { get; set; }

    public string status { get; set; }
    public string type { get; set; }
    public string link { get; set; }
    public Title title { get; set; }
    public Content content { get; set; }
    public Excerpt excerpt { get; set; }

    public Meta meta { get; set; }
    public int[] categories { get; set; }
    public object[] tags { get; set; }
    public string[] class_list { get; set; }

    public Yoast_Head_Json yoast_head_json { get; set; }
    public _Links _links { get; set; }
}

public class Guid
{
    public string rendered { get; set; }
}

public class Title
{
    public string rendered { get; set; }
}

public class Content
{
    public string rendered { get; set; }
    public bool _protected { get; set; }
}

public class Excerpt
{
    public string rendered { get; set; }
    public bool _protected { get; set; }
}

public class Meta
{
    public string footnotes { get; set; }
}

public class Yoast_Head_Json
{
    public string title { get; set; }
    public Robots robots { get; set; }
    public string canonical { get; set; }
    public string og_locale { get; set; }
    public string og_type { get; set; }
    public string og_title { get; set; }
    public string og_description { get; set; }
    public string og_url { get; set; }
    public string og_site_name { get; set; }
    public DateTime article_published_time { get; set; }
    public DateTime article_modified_time { get; set; }
    public Og_Image[] og_image { get; set; }
    public string author { get; set; }
    public string twitter_card { get; set; }
    public Twitter_Misc twitter_misc { get; set; }
    public Schema schema { get; set; }
    public string description { get; set; }
}

public class Robots
{
    public string index { get; set; }
    public string follow { get; set; }
    public string maxsnippet { get; set; }
    public string maximagepreview { get; set; }
    public string maxvideopreview { get; set; }
}

public class Twitter_Misc
{
    public string _نوشته‌شدهبدست { get; set; }
    public string زمانتقریبیبرایخواندن { get; set; }
}

public class Schema
{
    public string context { get; set; }
    public Graph[] graph { get; set; }
}

public class Graph
{
    public string type { get; set; }
    public string id { get; set; }
    public string url { get; set; }
    public string name { get; set; }
    public Ispartof isPartOf { get; set; }
    public Primaryimageofpage primaryImageOfPage { get; set; }
    public Image image { get; set; }
    public string thumbnailUrl { get; set; }
    public DateTime datePublished { get; set; }
    public DateTime dateModified { get; set; }
    public Author author { get; set; }
    public Breadcrumb breadcrumb { get; set; }
    public string inLanguage { get; set; }
    public Potentialaction[] potentialAction { get; set; }
    public string contentUrl { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string caption { get; set; }
    public Itemlistelement[] itemListElement { get; set; }
    public string description { get; set; }
    public string[] sameAs { get; set; }
}

public class Ispartof
{
    public string id { get; set; }
}

public class Primaryimageofpage
{
    public string id { get; set; }
}

public class Image
{
    public string id { get; set; }
}

public class Author
{
    public string id { get; set; }
}

public class Breadcrumb
{
    public string id { get; set; }
}

public class Potentialaction
{
    public string type { get; set; }
    public object target { get; set; }
    public QueryInput queryinput { get; set; }
}

public class QueryInput
{
    public string type { get; set; }
    public bool valueRequired { get; set; }
    public string valueName { get; set; }
}

public class Itemlistelement
{
    public string type { get; set; }
    public int position { get; set; }
    public string name { get; set; }
    public string item { get; set; }
}

public class Og_Image
{
    public int width { get; set; }
    public int height { get; set; }
    public string url { get; set; }
    public string type { get; set; }
}

public class _Links
{
    public Self[] self { get; set; }
    public Collection[] collection { get; set; }
    public About[] about { get; set; }
    public Author1[] author { get; set; }
    public Reply[] replies { get; set; }
    public VersionHistory[] versionhistory { get; set; }
    public PredecessorVersion[] predecessorversion { get; set; }
    public WpFeaturedmedia[] wpfeaturedmedia { get; set; }
    public WpAttachment[] wpattachment { get; set; }
    public WpTerm[] wpterm { get; set; }
    public Cury[] curies { get; set; }
}

public class Self
{
    public string href { get; set; }
    public Targethints targetHints { get; set; }
}

public class Targethints
{
    public string[] allow { get; set; }
}

public class Collection
{
    public string href { get; set; }
}

public class About
{
    public string href { get; set; }
}

public class Author1
{
    public bool embeddable { get; set; }
    public string href { get; set; }
}

public class Reply
{
    public bool embeddable { get; set; }
    public string href { get; set; }
}

public class VersionHistory
{
    public int count { get; set; }
    public string href { get; set; }
}

public class PredecessorVersion
{
    public int id { get; set; }
    public string href { get; set; }
}

public class WpFeaturedmedia
{
    public bool embeddable { get; set; }
    public string href { get; set; }
}

public class WpAttachment
{
    public string href { get; set; }
}

public class WpTerm
{
    public string taxonomy { get; set; }
    public bool embeddable { get; set; }
    public string href { get; set; }
}

public class Cury
{
    public string name { get; set; }
    public string href { get; set; }
    public bool templated { get; set; }
}
