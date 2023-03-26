using AutoMapper;
using eCommerce.Domain.Abstractions.Paginations;
using eCommerce.Domain.Domains;
using eCommerce.Model.Brands;
using eCommerce.Model.Categories;
using eCommerce.Model.Inventories;
using eCommerce.Model.Paginations;
using eCommerce.Model.Products;
using eCommerce.Model.PurchaseOrders;
using eCommerce.Model.Roles;
using eCommerce.Model.Suppliers;
using eCommerce.Model.Users;
using eCommerce.Shared.Extensions;
using PurchaseOrderDetailsModel = eCommerce.Model.PurchaseOrderDetails.PurchaseOrderDetailsModel;

namespace eCommerce.Service.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region CREATE MAPPER USER
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<IPagedList<User>, PaginationModel<UserModel>>().ReverseMap();
        CreateMap<UserRegistrationModel, User>()
            .AfterMap((src, dest) =>
            {
                dest.Username = src.Email;
                dest.PasswordHash = src.Password.HashMD5();
                dest.EmailConfirmed = false;
            });

        CreateMap<EditUserModel, User>()
            .AfterMap((src, dest) =>
            {
                dest.EmailConfirmed = true;
                dest.PasswordHash = src.Password.HashMD5();
                dest.Avatar = default!;
                dest.TotalAmountOwed = 0;
                dest.Status = false;
            });
        
        CreateMap<EditProfileModel, User>()
            .AfterMap((src, dest) =>
            {
                dest.Avatar = default!;
                dest.EmailConfirmed = true;
            });
        #endregion

        #region CREATE MAPPER ROLE
        CreateMap<Role, EditRoleModel>().ReverseMap();
        CreateMap<Role, AddRoleModel>().ReverseMap();
        CreateMap<Role, RoleModel>().ReverseMap();
        #endregion

        #region CREATE MAPPPER USERROLE

        

        #endregion

        #region CREATE MAPPER BRAND
        CreateMap<Brand, BrandModel>().ReverseMap();
        CreateMap<PagedList<Brand>, PaginationModel<BrandModel>>().ReverseMap();
        #endregion

        #region CREATE MAPPER CATEGORY
        CreateMap<Category, CategoryModel>().ReverseMap();
        CreateMap<PaginationModel<CategoryModel>, PagedList<Category>>().ReverseMap();
        #endregion

        #region CREATE MAPPER SUPPLIER
        CreateMap<Supplier, SupplierModel>().ReverseMap();
        CreateMap<PaginationModel<SupplierModel>, PagedList<Supplier>>().ReverseMap();
        #endregion

        #region CREATE MAPPER PRODUCT
        CreateMap<Product, ProductModel>().ReverseMap();
        CreateMap<PaginationModel<ProductModel>, PagedList<Product>>().ReverseMap();
        #endregion

        #region CREATE MAPPER INVENTORY
        CreateMap<Inventory, InventoryModel>().ReverseMap();
        #endregion

        #region CREATE MAPPER PURCHASE ORDER
        CreateMap<PurchaseOrderDetail, eCommerce.Model.PurchaseOrderDetails.PurchaseOrderDetailsModel>().ReverseMap();
        CreateMap<PurchaseOrder, PurchaseOrderModel>().ReverseMap();
        CreateMap<PaginationModel<PurchaseOrderModel>, PagedList<PurchaseOrder>>().ReverseMap();
        #endregion
    }
}