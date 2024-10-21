

using FlowSocial.Application.Services.Implement;
using FlowSocial.Application.Services.InterfaceService;
using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Domain.Models;
using FlowSocial.infrastructure.Repository;
using FlowSocial.Infrastructure.DataContext;
using FlowSocial.Infrastructure.Emails;
using FlowSocial.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Octagram.Application.Services;
using System;

using System.Text;


namespace FlowSocial.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<FlowSocialContext>(options =>
            {
                options.UseSqlServer(
                config.GetConnectionString("DefaultConnection")
                );
            });

           
          //  services.AddIdentityCore<ApplicationUser>(opt=>opt.SignIn.RequireConfirmedEmail=true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddSignInManager();
          services.AddIdentity<ApplicationUser,IdentityRole>(options =>
          {
              options.SignIn.RequireConfirmedEmail = true;
          }).AddEntityFrameworkStores<FlowSocialContext>().AddDefaultTokenProviders();
         services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(3); // Extend to 3 days
            });

            services.AddAuthentication(op =>
            {

                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["Jwt:ValidIssuer"],
                    ValidAudience = config["Jwt:ValidAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]!))
                };



            });
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddCors(options =>
            {
                options.AddPolicy("Clean", bul => bul.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });


          

            services.AddScoped<IAccount,AccountRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IFolllowRepository, FollowRepository>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IStoryService, StoryService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();




            services.Configure<MailSettings>(config.GetSection("MailSettings"));
          

            return services;
        }
    }
}
