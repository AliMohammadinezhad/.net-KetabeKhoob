﻿using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Domain.CategoryAgg;

public class Category : AggregateRoot
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public long? ParentId { get; private set; }
    public List<Category> Childes { get; private set; }

    public Category(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        slug = slug?.ToSlug();
        Guard(title, slug, service);
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }

    public void Edit(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        slug = slug?.ToSlug();
        Guard(title, slug, service);
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }

    public void AddChild(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        Childes.Add(new Category(title, slug, seoData, service)
        {
            ParentId = Id
        });

    }


    private void Guard(string title, string slug, ICategoryDomainService service)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

        if (slug != Slug)
            if (service.IsSlugExist(slug))
                throw new SlugIsDuplicatedException();
    }
}