.class public Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;
.super Ljava/lang/Object;
.source "SearchView_OnCloseListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroidx/appcompat/widget/SearchView$OnCloseListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onClose:()Z:GetOnCloseHandler:AndroidX.AppCompat.Widget.SearchView/IOnCloseListenerInvoker, Xamarin.AndroidX.AppCompat\n"

    sput-object v0, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;

    const-string v2, "AndroidX.AppCompat.Widget.SearchView+IOnCloseListenerImplementor, Xamarin.AndroidX.AppCompat"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 17
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "AndroidX.AppCompat.Widget.SearchView+IOnCloseListenerImplementor, Xamarin.AndroidX.AppCompat"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_onClose()Z
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 39
    iget-object v0, p0, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 40
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 41
    :cond_0
    iget-object v0, p0, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 42
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 46
    iget-object v0, p0, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 47
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 48
    :cond_0
    return-void
.end method

.method public onClose()Z
    .locals 1

    .line 31
    invoke-direct {p0}, Lmono/androidx/appcompat/widget/SearchView_OnCloseListenerImplementor;->n_onClose()Z

    move-result v0

    return v0
.end method
