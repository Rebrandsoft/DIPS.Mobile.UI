.class public Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;
.super Landroid/graphics/drawable/ShapeDrawable$ShaderFactory;
.source "GradientStrokeDrawable_GradientShaderFactory.java"

# interfaces
.implements Lmono/android/IGCUserPeer;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 12
    const-string v0, "n_resize:(II)Landroid/graphics/Shader;:GetResize_IIHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;->__md_methods:Ljava/lang/String;

    .line 15
    const-class v1, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;

    const-string v2, "Xamarin.Forms.Platform.Android.GradientStrokeDrawable+GradientShaderFactory, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 16
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 21
    invoke-direct {p0}, Landroid/graphics/drawable/ShapeDrawable$ShaderFactory;-><init>()V

    .line 22
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;

    if-ne v0, v1, :cond_0

    .line 23
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.GradientStrokeDrawable+GradientShaderFactory, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 25
    :cond_0
    return-void
.end method

.method private native n_resize(II)Landroid/graphics/Shader;
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 38
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 39
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;->refList:Ljava/util/ArrayList;

    .line 40
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 41
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 45
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 46
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 47
    :cond_0
    return-void
.end method

.method public resize(II)Landroid/graphics/Shader;
    .locals 0

    .line 30
    invoke-direct {p0, p1, p2}, Lcrc643f46942d9dd1fff9/GradientStrokeDrawable_GradientShaderFactory;->n_resize(II)Landroid/graphics/Shader;

    move-result-object p1

    return-object p1
.end method
