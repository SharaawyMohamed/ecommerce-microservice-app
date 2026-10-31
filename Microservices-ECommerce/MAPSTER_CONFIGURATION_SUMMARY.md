# Mapster Mapping Configuration Summary

All services now have comprehensive Mapster mapping configurations implemented in the Application layer with proper DI registration.

## Configuration Overview

### Mapster IRegister Pattern
Each service's Application layer now has mapping configurations using the **IRegister pattern** for automatic discovery and registration:

```csharp
public class [Entity]MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		// Configure mapster mappings
		config.NewConfig<SourceEntity, TargetDTO>();
	}
}
```

## Services & Mapping Configurations

### 1. **Basket Service** ✅
- **File:** `Services/Basket/Basket.Application/Mappings/CartMapper.cs`
- **Mappings:**
  - `ShoppingCart` → `GetShoppingBasketResponse`
  - `ShoppingCartItem` → `ShoppingBasketItemResponse`
- **DI Registration:** `services.AddMapster()` in `DependencyInjection.cs`

### 2. **Catalog Service** ✅
- **File:** `Services/Catalog/Catalog.Application/Mappings/ProductMappingConfig.cs`
- **Mappings:**
  - `Product` → `GetProductByIdResponse`
- **DI Registration:** `services.AddMapster()` in `DependencyInjection.cs`
- **API:** Already using Mapster for query response mappings

### 3. **Discount Service** ✅
- **File:** `Services/Discount/Discount.Application/Mappings/CouponMappingConfig.cs`
- **DI Registration:** `services.AddMapster()` in `DependencyInjection.cs` (newly created)
- **API:** Registered `AddApplication()` in `Program.cs`

### 4. **Ordering Service** ✅
- **File:** `Services/Ordering/Ordering.Application/Mappings/OrderMappingConfig.cs`
- **DI Registration:** `services.AddMapster()` in `DependencyInjection.cs`
- **API:** Registered `AddApplication()` in `Program.cs`

## Dependency Injection Setup

### Application Layer DI Pattern
Each service's `DependencyInjection.cs` now includes:

```csharp
public static IServiceCollection AddApplication(this IServiceCollection services)
{
	// Register MediatR handlers
	services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(...));

	// Register Mapster with auto-discovery of IRegister implementations
	services.AddMapster();

	return services;
}
```

### API Layer Registration
All API `Program.cs` files call `.AddApplication()`:

```csharp
builder.Services
	.AddInfrustructure(builder.Configuration)
	.AddApplication()  // Registers Mapster
```

## Package Dependencies Added

- **Discount.Application:** Added Mapster v10.0.13 and Mapster.DependencyInjection v10.0.12
- **Ordering.Application:** Added Mapster v10.0.13 and Mapster.DependencyInjection v10.0.12
- **Basket & Catalog:** Already had Mapster packages

## How It Works

1. **IRegister Discovery:** When `services.AddMapster()` is called, Mapster automatically scans the assembly for all `IRegister` implementations
2. **Auto-Configuration:** Each mapping config's `Register()` method is called automatically
3. **Type Conversion:** Mapster creates adapters to convert between entity types (Domain → DTOs)
4. **Injection:** The configured `IMapper` is available throughout the application

## Example Usage in Handlers

```csharp
// In a MediatR Handler
var response = entity.Adapt<ResponseDTO>();  // Auto-maps using configured rules

// Bulk mapping
var responses = entities.Adapt<List<ResponseDTO>>();
```

## Files Created/Modified

### Created:
- `Services/Discount/Discount.Application/DependencyInjection.cs`
- `Services/Discount/Discount.Application/Mappings/CouponMappingConfig.cs`
- `Services/Ordering/Ordering.Application/Mappings/OrderMappingConfig.cs`

### Modified:
- `Services/Basket/Basket.Application/DependencyInjection.cs` - Added Mapster registration
- `Services/Basket/Basket.API/Program.cs` - Already had AddApplication()
- `Services/Catalog/Catalog.Application/DependencyInjection.cs` - Added Mapster registration
- `Services/Discount/Discount.API/Program.cs` - Added AddApplication() call
- `Services/Ordering/Ordering.Application/DependencyInjection.cs` - Added Mapster registration and fixed namespace
- `Services/Ordering/Ordering.API/Program.cs` - Added Ordering.Application namespace and confirmed AddApplication()

## Build Status
✅ **SUCCESS** - Solution builds without errors

## Best Practices Implemented

1. **Separation of Concerns:** Mappings live in Application layer, not API
2. **Convention-Based Mapping:** Mapster uses property name matching (no explicit configuration needed for matching properties)
3. **DI Auto-Discovery:** IRegister pattern allows automatic scanning instead of manual configuration
4. **Consistency:** All services follow the same mapping pattern

## Next Steps (Optional Enhancements)

1. **Add response DTOs** for missing query responses (GetProductByName, GetProductsByType, etc.)
2. **Configure custom mappings** for complex properties (nested objects, value transformations)
3. **Add API DTO to Domain mappings** for incoming command requests
4. **Create reusable mapping extensions** for common patterns (e.g., pagination, filtering)

## Common Configuration Examples

```csharp
// Simple 1:1 mapping (uses property name matching)
config.NewConfig<Product, GetProductByIdResponse>();

// Mapping with property name transformation
config.NewConfig<Product, ProductDTO>()
	.Map(dest => dest.DisplayName, src => src.Name);

// Ignoring properties
config.NewConfig<Product, ProductDTO>()
	.Ignore(dest => dest.ExtraField);

// Value transformation
config.NewConfig<Order, OrderDTO>()
	.Map(dest => dest.TotalFormatted, src => src.Total.ToString("C"));
```

## Validation

Build output confirms all services compile successfully with Mapster configurations properly registered and discoverable via IRegister pattern.
