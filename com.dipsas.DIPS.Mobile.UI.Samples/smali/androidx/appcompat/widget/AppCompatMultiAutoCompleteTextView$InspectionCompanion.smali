.class public final Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;
.super Ljava/lang/Object;
.source "AppCompatMultiAutoCompleteTextView$InspectionCompanion.java"

# interfaces
.implements Landroid/view/inspector/InspectionCompanion;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Landroid/view/inspector/InspectionCompanion<",
        "Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;",
        ">;"
    }
.end annotation


# instance fields
.field private mBackgroundTintId:I

.field private mBackgroundTintModeId:I

.field private mPropertiesMapped:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 19
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 20
    const/4 v0, 0x0

    iput-boolean v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mPropertiesMapped:Z

    return-void
.end method


# virtual methods
.method public mapProperties(Landroid/view/inspector/PropertyMapper;)V
    .locals 2
    .param p1, "propertyMapper"    # Landroid/view/inspector/PropertyMapper;

    .line 28
    sget v0, Landroidx/appcompat/R$attr;->backgroundTint:I

    const-string v1, "backgroundTint"

    invoke-interface {p1, v1, v0}, Landroid/view/inspector/PropertyMapper;->mapObject(Ljava/lang/String;I)I

    move-result v0

    iput v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mBackgroundTintId:I

    .line 29
    sget v0, Landroidx/appcompat/R$attr;->backgroundTintMode:I

    const-string v1, "backgroundTintMode"

    invoke-interface {p1, v1, v0}, Landroid/view/inspector/PropertyMapper;->mapObject(Ljava/lang/String;I)I

    move-result v0

    iput v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mBackgroundTintModeId:I

    .line 30
    const/4 v0, 0x1

    iput-boolean v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mPropertiesMapped:Z

    .line 31
    return-void
.end method

.method public readProperties(Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;Landroid/view/inspector/PropertyReader;)V
    .locals 2
    .param p1, "appCompatMultiAutoCompleteTextView"    # Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;
    .param p2, "propertyReader"    # Landroid/view/inspector/PropertyReader;

    .line 37
    iget-boolean v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mPropertiesMapped:Z

    if-eqz v0, :cond_0

    .line 40
    iget v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mBackgroundTintId:I

    invoke-virtual {p1}, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;->getBackgroundTintList()Landroid/content/res/ColorStateList;

    move-result-object v1

    invoke-interface {p2, v0, v1}, Landroid/view/inspector/PropertyReader;->readObject(ILjava/lang/Object;)V

    .line 41
    iget v0, p0, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->mBackgroundTintModeId:I

    invoke-virtual {p1}, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;->getBackgroundTintMode()Landroid/graphics/PorterDuff$Mode;

    move-result-object v1

    invoke-interface {p2, v0, v1}, Landroid/view/inspector/PropertyReader;->readObject(ILjava/lang/Object;)V

    .line 42
    return-void

    .line 38
    :cond_0
    new-instance v0, Landroid/view/inspector/InspectionCompanion$UninitializedPropertyMapException;

    invoke-direct {v0}, Landroid/view/inspector/InspectionCompanion$UninitializedPropertyMapException;-><init>()V

    throw v0
.end method

.method public bridge synthetic readProperties(Ljava/lang/Object;Landroid/view/inspector/PropertyReader;)V
    .locals 0

    .line 17
    check-cast p1, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;

    invoke-virtual {p0, p1, p2}, Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView$InspectionCompanion;->readProperties(Landroidx/appcompat/widget/AppCompatMultiAutoCompleteTextView;Landroid/view/inspector/PropertyReader;)V

    return-void
.end method
