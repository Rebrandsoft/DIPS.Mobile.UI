.class public Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;
.super Ljava/lang/Object;
.source "ChipGroup_OnCheckedChangeListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Lcom/google/android/material/chip/ChipGroup$OnCheckedChangeListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onCheckedChanged:(Lcom/google/android/material/chip/ChipGroup;I)V:GetOnCheckedChanged_Lcom_google_android_material_chip_ChipGroup_IHandler:Google.Android.Material.Chip.ChipGroup/IOnCheckedChangeListenerInvoker, Xamarin.Google.Android.Material\n"

    sput-object v0, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;

    const-string v2, "Google.Android.Material.Chip.ChipGroup+IOnCheckedChangeListenerImplementor, Xamarin.Google.Android.Material"

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

    const-class v1, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Google.Android.Material.Chip.ChipGroup+IOnCheckedChangeListenerImplementor, Xamarin.Google.Android.Material"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_onCheckedChanged(Lcom/google/android/material/chip/ChipGroup;I)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 39
    iget-object v0, p0, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 40
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 41
    :cond_0
    iget-object v0, p0, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 42
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 46
    iget-object v0, p0, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 47
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 48
    :cond_0
    return-void
.end method

.method public onCheckedChanged(Lcom/google/android/material/chip/ChipGroup;I)V
    .locals 0

    .line 31
    invoke-direct {p0, p1, p2}, Lmono/com/google/android/material/chip/ChipGroup_OnCheckedChangeListenerImplementor;->n_onCheckedChanged(Lcom/google/android/material/chip/ChipGroup;I)V

    .line 32
    return-void
.end method
