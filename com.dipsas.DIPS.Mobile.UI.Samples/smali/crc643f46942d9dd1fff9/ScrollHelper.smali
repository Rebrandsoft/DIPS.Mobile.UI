.class public Lcrc643f46942d9dd1fff9/ScrollHelper;
.super Landroidx/recyclerview/widget/RecyclerView$OnScrollListener;
.source "ScrollHelper.java"

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
    const-string v0, "n_onScrolled:(Landroidx/recyclerview/widget/RecyclerView;II)V:GetOnScrolled_Landroidx_recyclerview_widget_RecyclerView_IIHandler\n"

    sput-object v0, Lcrc643f46942d9dd1fff9/ScrollHelper;->__md_methods:Ljava/lang/String;

    .line 15
    const-class v1, Lcrc643f46942d9dd1fff9/ScrollHelper;

    const-string v2, "Xamarin.Forms.Platform.Android.ScrollHelper, Xamarin.Forms.Platform.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 16
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 21
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$OnScrollListener;-><init>()V

    .line 22
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ScrollHelper;

    if-ne v0, v1, :cond_0

    .line 23
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Xamarin.Forms.Platform.Android.ScrollHelper, Xamarin.Forms.Platform.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 25
    :cond_0
    return-void
.end method

.method public constructor <init>(Landroidx/recyclerview/widget/RecyclerView;)V
    .locals 2

    .line 29
    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$OnScrollListener;-><init>()V

    .line 30
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lcrc643f46942d9dd1fff9/ScrollHelper;

    if-ne v0, v1, :cond_0

    .line 31
    const/4 v0, 0x1

    new-array v0, v0, [Ljava/lang/Object;

    const/4 v1, 0x0

    aput-object p1, v0, v1

    const-string p1, "Xamarin.Forms.Platform.Android.ScrollHelper, Xamarin.Forms.Platform.Android"

    const-string v1, "AndroidX.RecyclerView.Widget.RecyclerView, Xamarin.AndroidX.RecyclerView"

    invoke-static {p1, v1, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 33
    :cond_0
    return-void
.end method

.method private native n_onScrolled(Landroidx/recyclerview/widget/RecyclerView;II)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 46
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollHelper;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 47
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollHelper;->refList:Ljava/util/ArrayList;

    .line 48
    :cond_0
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollHelper;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 49
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 53
    iget-object v0, p0, Lcrc643f46942d9dd1fff9/ScrollHelper;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 54
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 55
    :cond_0
    return-void
.end method

.method public onScrolled(Landroidx/recyclerview/widget/RecyclerView;II)V
    .locals 0

    .line 38
    invoke-direct {p0, p1, p2, p3}, Lcrc643f46942d9dd1fff9/ScrollHelper;->n_onScrolled(Landroidx/recyclerview/widget/RecyclerView;II)V

    .line 39
    return-void
.end method
