﻿using System;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Api.DAL.Common.Entities;

public class AppUserClaimEntity : IdentityUserClaim<Guid>
{
}