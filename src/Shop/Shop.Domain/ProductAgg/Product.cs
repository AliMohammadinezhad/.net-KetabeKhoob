﻿using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Domain.ProductAgg;

public class Product : AggregateRoot
{
    public string Title { get; private set; }
    public string ImageName { get; private set; }
    public string Description { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long SecondarySubCategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public List<ProductSpecification> Specifications { get; private set; }

    private Product()
    {
    }

    public Product(
        string title,
        string imageName,
        string description,
        long categoryId,
        long subCategoryId,
        long secondarySubCategoryId,
        string slug,
        SeoData seoData,
        IProductDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        Guard(title, slug, description, domainService);
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }


    public void Edit(
        string title,
        string description,
        long categoryId,
        long subCategoryId,
        long secondarySubCategoryId,
        string slug,
        SeoData seoData,
        IProductDomainService domainService)
    {
        Guard(title, slug, description,domainService);
        Title = title;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void SetProductImage(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
    }

    public void AddImage(ProductImage image)
    {
        image.ProductId = Id;
        Images.Add(image);
    }

    public string RemoveImage(long imageId)
    {
        var image = Images.Find(x => x.Id == imageId);
        if (image is null)
            throw new NullOrEmptyDomainDataException("عکس یافت نشد.");

        Images.Remove(image);
        return image.ImageName;
    }

    public void SetSpecification(List<ProductSpecification> specifications)
    {
        specifications.ForEach(s => s.ProductId = Id);
        Specifications = specifications;
    }

    private void Guard(
        string title,
        string slug,
        string description,
        IProductDomainService domainService
    )
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        
        if(slug.ToSlug() != Slug)
            if (domainService.SlugIsExist(slug.ToSlug()))
                throw new SlugIsDuplicatedException();
    }

}