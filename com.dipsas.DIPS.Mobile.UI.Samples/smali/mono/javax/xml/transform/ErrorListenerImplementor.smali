.class public Lmono/javax/xml/transform/ErrorListenerImplementor;
.super Ljava/lang/Object;
.source "ErrorListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Ljavax/xml/transform/ErrorListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_error:(Ljavax/xml/transform/TransformerException;)V:GetError_Ljavax_xml_transform_TransformerException_Handler:Javax.Xml.Transform.IErrorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_fatalError:(Ljavax/xml/transform/TransformerException;)V:GetFatalError_Ljavax_xml_transform_TransformerException_Handler:Javax.Xml.Transform.IErrorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\nn_warning:(Ljavax/xml/transform/TransformerException;)V:GetWarning_Ljavax_xml_transform_TransformerException_Handler:Javax.Xml.Transform.IErrorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/javax/xml/transform/ErrorListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 18
    const-class v1, Lmono/javax/xml/transform/ErrorListenerImplementor;

    const-string v2, "Javax.Xml.Transform.IErrorListenerImplementor, Mono.Android"

    invoke-static {v2, v1, v0}, Lmono/android/Runtime;->register(Ljava/lang/String;Ljava/lang/Class;Ljava/lang/String;)V

    .line 19
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 25
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    const-class v1, Lmono/javax/xml/transform/ErrorListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 26
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Javax.Xml.Transform.IErrorListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 28
    :cond_0
    return-void
.end method

.method private native n_error(Ljavax/xml/transform/TransformerException;)V
.end method

.method private native n_fatalError(Ljavax/xml/transform/TransformerException;)V
.end method

.method private native n_warning(Ljavax/xml/transform/TransformerException;)V
.end method


# virtual methods
.method public error(Ljavax/xml/transform/TransformerException;)V
    .locals 0

    .line 33
    invoke-direct {p0, p1}, Lmono/javax/xml/transform/ErrorListenerImplementor;->n_error(Ljavax/xml/transform/TransformerException;)V

    .line 34
    return-void
.end method

.method public fatalError(Ljavax/xml/transform/TransformerException;)V
    .locals 0

    .line 41
    invoke-direct {p0, p1}, Lmono/javax/xml/transform/ErrorListenerImplementor;->n_fatalError(Ljavax/xml/transform/TransformerException;)V

    .line 42
    return-void
.end method

.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 57
    iget-object v0, p0, Lmono/javax/xml/transform/ErrorListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 58
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/javax/xml/transform/ErrorListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 59
    :cond_0
    iget-object v0, p0, Lmono/javax/xml/transform/ErrorListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 60
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 64
    iget-object v0, p0, Lmono/javax/xml/transform/ErrorListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 65
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 66
    :cond_0
    return-void
.end method

.method public warning(Ljavax/xml/transform/TransformerException;)V
    .locals 0

    .line 49
    invoke-direct {p0, p1}, Lmono/javax/xml/transform/ErrorListenerImplementor;->n_warning(Ljavax/xml/transform/TransformerException;)V

    .line 50
    return-void
.end method
