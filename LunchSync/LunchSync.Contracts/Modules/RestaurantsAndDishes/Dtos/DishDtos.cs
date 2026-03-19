namespace LunchSync.Contracts.Modules.RestaurantsAndDishes.Dtos;

public sealed record DishDto
{
    // Id cua mon an
    public Guid Id { get; init; }
    // Ten mon an
    public string Name { get; init; } = null!;
    // Danh muc mon an
    public string Category { get; init; } = null!;
    // Muc do nuoc
    public double Soupy { get; init; }
    // Muc do nong/lanh
    public double Temperature { get; init; }
    // Muc do no
    public double Heaviness { get; init; }
    // Cuong do huong vi
    public double FlavorIntensity { get; init; }
    // Muc do cay
    public double Spicy { get; init; }
    // Do phuc tap ket cau
    public double TextureComplexity { get; init; }
    // Thoi gian can thiet de an
    public double TimeRequired { get; init; }
    // Muc do moi la
    public double Novelty { get; init; }
    // Muc do tot cho suc khoe
    public double Healthy { get; init; }
    // Muc do an chung
    public double Communal { get; init; }
    // Phien ban ho so
    public int Version { get; init; }
    // Thoi diem tao
    public DateTime CreatedAt { get; init; }
    // Thoi diem cap nhat
    public DateTime? UpdatedAt { get; init; }
}

public sealed record CreateDishRequest
{
    // Ten mon an
    public string Name { get; init; } = null!;
    // Danh muc mon an
    public string Category { get; init; } = null!;
    // Muc do nuoc
    public double Soupy { get; init; }
    // Muc do nong/lanh
    public double Temperature { get; init; }
    // Muc do no
    public double Heaviness { get; init; }
    // Cuong do huong vi
    public double FlavorIntensity { get; init; }
    // Muc do cay
    public double Spicy { get; init; }
    // Do phuc tap ket cau
    public double TextureComplexity { get; init; }
    // Thoi gian can thiet de an
    public double TimeRequired { get; init; }
    // Muc do moi la
    public double Novelty { get; init; }
    // Muc do tot cho suc khoe
    public double Healthy { get; init; }
    // Muc do an chung
    public double Communal { get; init; }
    // Phien ban ho so
    public int Version { get; init; }
}

public sealed record UpdateDishRequest
{
    // Ten mon an
    public string Name { get; init; } = null!;
    // Danh muc mon an
    public string Category { get; init; } = null!;
    // Muc do nuoc
    public double Soupy { get; init; }
    // Muc do nong/lanh
    public double Temperature { get; init; }
    // Muc do no
    public double Heaviness { get; init; }
    // Cuong do huong vi
    public double FlavorIntensity { get; init; }
    // Muc do cay
    public double Spicy { get; init; }
    // Do phuc tap ket cau
    public double TextureComplexity { get; init; }
    // Thoi gian can thiet de an
    public double TimeRequired { get; init; }
    // Muc do moi la
    public double Novelty { get; init; }
    // Muc do tot cho suc khoe
    public double Healthy { get; init; }
    // Muc do an chung
    public double Communal { get; init; }
    // Phien ban ho so
    public int Version { get; init; }
}
