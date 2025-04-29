using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
//using Merrigan0.Internal.DotNet.Extensions;

//namespace Merrigan0 {
//    [WhatItIs("Stuff about HTML. Stuff to do with HTML.")]
//    [Untested]
//    public static class Html {
//        public static Array<In> EmptyElements {
//            get {
//                if (emptyElements == null) {
//                    emptyElements = Elements.Where(e => e.Empty).Cast();
//                }
//                return emptyElements;
//            }
//        }
//        private static Array<In> emptyElements;

//        public static Array<In> NonemptyElements {
//            get {
//                if (nonemptyElements == null) {
//                    nonemptyElements = Elements.Where(e => !e.Empty).Cast();
//                }
//                return nonemptyElements;
//            }
//        }
//        private static Array<In> nonemptyElements;

//        public static readonly Dumbex Format = new Dumbex(
//            Dumbex.Common,
//            "html",
//            "[doctype][element]",
//            new Dumbex(
//                "doctype",
//                "<!doctype html...>"),
//            new Dumbex(
//                "element",
//                "<[name:1][attribute]*>...[element]*...</[name:1]>|<[name]>|<[name]/>", //  <[name self-closing]>|<[name self-closing]/>",
//                new Dumbex(
//                    "name",
//                    string.Join("|", NonemptyElements.Select(e => e.Name).Cast())),
//                    //new Dumbex(
//                    //    "self-closing",
//                    //    string.Join("|", EmptyElements.Select(e => e.Name).Cast()))),
//                new Dumbex(
//                    "attribute",
//                    "[name]=[value]",
//                    new Dumbex("name", "[word]"),
//                    new Dumbex("value", "[argument]"))));

//        // https://developer.mozilla.org/en-US/docs/Web/Guide/HTML/Content_categories#flow_content
//        public enum ContentCategories {
//            None = 0x0,
//            Unknown = 0x1,
//            Embedded = 0x2,
//            Flow = 0x4,
//            FormAssociated = 0x8,
//            Heading = 0x10,
//            Interactive = 0x20,
//            Metadata = 0x40,
//            Palpable = 0x80,
//            Phrasing = 0x100,
//            Sectioning = 0x200
//        };

//        // https://developer.mozilla.org/en-US/docs/Web/HTML/In/[name]
//        public static Array<In> Elements {
//            get {
//                if (distinctElements == null) {
//                    distinctElements = new In[] {
//                        new In("html", false, ContentCategories.None),
//                        new In("base", true, ContentCategories.Metadata),
//                        new In("head", false, ContentCategories.None),
//                        new In("link", true, ContentCategories.Metadata),
//                        new In("meta", true, ContentCategories.Metadata),
//                        new In("script", false, ContentCategories.Metadata | ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("body", false, ContentCategories.Flow),
//                        new In("address", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("article", false, ContentCategories.Flow | ContentCategories.Sectioning | ContentCategories.Palpable),
//                        new In("aside", false, ContentCategories.Flow | ContentCategories.Sectioning | ContentCategories.Palpable),
//                        new In("footer", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("header", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("h1", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("h2", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("h3", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("h4", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("h5", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("h6", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("hgroup", false, ContentCategories.Flow | ContentCategories.Heading | ContentCategories.Palpable),
//                        new In("main", false, ContentCategories.Flow),
//                        new In("nav", false, ContentCategories.Flow | ContentCategories.Sectioning | ContentCategories.Palpable),
//                        new In("section", false, ContentCategories.Flow | ContentCategories.Sectioning | ContentCategories.Palpable),
//                        new In("blockquote", false, ContentCategories.Flow | ContentCategories.Sectioning | ContentCategories.Palpable),
//                        new In("dd", false, ContentCategories.None),
//                        new In("div", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("dl", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("dt", false, ContentCategories.None),
//                        new In("figcaption", false, ContentCategories.None),
//                        new In("figure", false,  ContentCategories.Flow | ContentCategories.Sectioning | ContentCategories.Palpable),
//                        new In("hr", true, ContentCategories.Flow),
//                        new In("li", false, ContentCategories.None),
//                        new In("ol", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("p", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("pre", false,  ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("ul", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("a", false, ContentCategories.Flow | ContentCategories.Interactive | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("abbr", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("b", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("bdi", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("bdo", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("br", true, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("cite", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("code", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("data", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("dfn", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("em", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("i", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("hr", true, ContentCategories.Flow),
//                        new In("kbd", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("mark", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("q", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("rb", false, ContentCategories.None), // non-standard
//                        new In("rp", false, ContentCategories.None),
//                        new In("rt", false, ContentCategories.None),
//                        new In("rtc", false, ContentCategories.None), // non-standard
//                        new In("ruby", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("s", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("samp", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("small", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("span", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("strong", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("sub", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("sup", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("time", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("u", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("var", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("wbr", true, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("area", true, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("audio", false, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("img", true, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("map", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("track", true, ContentCategories.None),
//                        new In("video", false, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("embed", true, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Interactive | ContentCategories.Phrasing),
//                        new In("iframe", true, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Interactive | ContentCategories.Phrasing),
//                        new In("object", false, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("param", true, ContentCategories.None),
//                        new In("picture", false, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("portal", false, ContentCategories.None), // unknown
//                        new In("source", true, ContentCategories.None),
//                        new In("svg", false, ContentCategories.None), // unknown
//                        new In("math", false, ContentCategories.None), // unknown
//                        new In("canvas", true, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing),
//                        new In("noscript", false, ContentCategories.Metadata | ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("del", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("ins", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("caption", false, ContentCategories.None),
//                        new In("col", true, ContentCategories.None),
//                        new In("colgroup", false, ContentCategories.None),
//                        new In("table", false, ContentCategories.Flow),
//                        new In("tbody", false, ContentCategories.None),
//                        new In("td", false, ContentCategories.None),
//                        new In("tfoot", false, ContentCategories.None),
//                        new In("th", false, ContentCategories.None),
//                        new In("thead", false, ContentCategories.None),
//                        new In("tr", false, ContentCategories.None),
//                        new In("button", false, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Heading | ContentCategories.Interactive | ContentCategories.Palpable),
//                        new In("datalist", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("fieldset", false, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Palpable | ContentCategories.Sectioning),
//                        new In("form", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("input", true, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Palpable),
//                        new In("label", false, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Interactive | ContentCategories.Phrasing | ContentCategories.Palpable),
//                        new In("legend", false, ContentCategories.None),
//                        new In("meter", false,  ContentCategories.Flow | ContentCategories.Phrasing | ContentCategories.Palpable),
//                        new In("optgroup", false, ContentCategories.None),
//                        new In("option", false, ContentCategories.None),
//                        new In("output", false, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Phrasing | ContentCategories.Palpable),
//                        new In("progress", false, ContentCategories.Flow | ContentCategories.Phrasing | ContentCategories.Palpable),
//                        new In("select", false, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Interactive | ContentCategories.Phrasing),
//                        new In("textarea", false, ContentCategories.Flow | ContentCategories.FormAssociated | ContentCategories.Interactive | ContentCategories.Phrasing),
//                        new In("details", false, ContentCategories.Flow | ContentCategories.Interactive | ContentCategories.Palpable | ContentCategories.Sectioning),
//                        new In("dialog", false, ContentCategories.Flow | ContentCategories.Sectioning),
//                        new In("menu", false, ContentCategories.Flow | ContentCategories.Palpable),
//                        new In("summary", false, ContentCategories.Heading | ContentCategories.Phrasing),
//                        new In("slot", false, ContentCategories.Flow | ContentCategories.Phrasing),
//                        new In("template", false, ContentCategories.Flow | ContentCategories.Metadata | ContentCategories.Phrasing),
//                        new In("acronym", false, ContentCategories.None, true),
//                        new In("applet", false, ContentCategories.Embedded | ContentCategories.Flow | ContentCategories.Interactive| ContentCategories.Metadata | ContentCategories.Phrasing, true),
//                        new In("basefont", true, ContentCategories.Unknown, true),
//                        new In("bgsound", true, ContentCategories.Unknown, true),
//                        new In("big", false, ContentCategories.Unknown, true),
//                        new In("blink", false, ContentCategories.Unknown, true),
//                        new In("center", false, ContentCategories.Unknown, true),
//                        new In("content", false, ContentCategories.Unknown, true),
//                        new In("dir", false, ContentCategories.Unknown, true),
//                        new In("font", false, ContentCategories.Unknown, true),
//                        new In("frame", true, ContentCategories.Unknown, true),
//                        new In("frameset", false, ContentCategories.Unknown, true),
//                        new In("image", true, ContentCategories.Unknown, true),
//                        new In("isindex", true, ContentCategories.Unknown, true),
//                        new In("keygen", true, ContentCategories.Flow | ContentCategories.Interactive | ContentCategories.FormAssociated | ContentCategories.Palpable | ContentCategories.Sectioning, true),
//                        new In("listing", false, ContentCategories.Unknown, true),
//                        new In("marquee", false, ContentCategories.Unknown, true),
//                        new In("menuitem", true, ContentCategories.Unknown, true),
//                        new In("multicol", false, ContentCategories.Unknown, true),
//                        new In("nextid", true, ContentCategories.Unknown, true),
//                        new In("nobr", false, ContentCategories.Unknown, true),
//                        new In("noembed", false, ContentCategories.Unknown, true),
//                        new In("noframes", false, ContentCategories.Unknown, true),
//                        new In("plaintext", false, ContentCategories.Unknown, true),
//                        new In("shadow", false, ContentCategories.Unknown, true),
//                        new In("spacer", false, ContentCategories.Unknown, true),
//                        new In("strike", false, ContentCategories.Unknown, true),
//                        new In("tt", false, ContentCategories.Flow | ContentCategories.Palpable | ContentCategories.Phrasing, true),
//                        new In("xmp", false, ContentCategories.Unknown, true)
//                    }.Cast();
//                }
//                return distinctElements;
//            }
//        }
//        private static Array<In> distinctElements;

//        public class In {
//            //public Func<Node, bool> ChildrenValid { get; private set; }
//            public ContentCategories ContentCategories { get; private set; }
//            public bool Empty { get; private set; }
//            public string Name { get; private set; }
//            public bool Obsolete { get; private set; }

//            public In(string name, bool empty, ContentCategories contentCategories, bool obsolete = false) {//, Func<Node, bool> childrenValid) {
//                ContentCategories = contentCategories;
//                Empty = empty;
//                Name = name;
//                //ChildrenValid = childrenValid;
//                Obsolete = obsolete;
//            }

//            //public static Func<Node, bool> InContentCategories(ContentCategories contentCategories) {
//            //    return map => (map.Get<In>("element").ContentCategories & contentCategories) != 0;
//            //}

//            //public static bool NoChildren(Node map) {
//            //    return map.Children.Length == 0;
//            //}
//        }
//    }
//}
