﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.CommentAgg.Enums;

namespace Shop.Domain.CommentAgg;

public class Comment : AggregateRoot
{
    public long UserId { get; private set; }
    public long ProductId { get; private set; }
    public string Text { get; private set; }
    public CommentStatus Status { get; private set; }
    public DateTime ModifiedDateTime { get; private set; }

    private Comment()
    {
    }

    public Comment(long userId, long productId, string text)
    {
        NullOrEmptyDomainDataException.CheckString(text, nameof(text));
        UserId = userId;
        ProductId = productId;
        Text = text;
        Status = CommentStatus.Pending;
    }

    public void Edit(string text)
    {
        NullOrEmptyDomainDataException.CheckString(text, nameof(text));
        Text = text;
        ModifiedDateTime = DateTime.Now;
    }

    public void ChangeStatus(CommentStatus status)
    {
        Status = status;
        ModifiedDateTime = DateTime.Now;
    }
}