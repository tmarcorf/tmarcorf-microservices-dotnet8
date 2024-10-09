using System.Collections.Generic;

namespace Marco.HATEOAS.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
