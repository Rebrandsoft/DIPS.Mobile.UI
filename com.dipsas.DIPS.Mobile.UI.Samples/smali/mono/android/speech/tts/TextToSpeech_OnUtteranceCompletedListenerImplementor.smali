.class public Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;
.super Ljava/lang/Object;
.source "TextToSpeech_OnUtteranceCompletedListenerImplementor.java"

# interfaces
.implements Lmono/android/IGCUserPeer;
.implements Landroid/speech/tts/TextToSpeech$OnUtteranceCompletedListener;


# static fields
.field public static final __md_methods:Ljava/lang/String;


# instance fields
.field private refList:Ljava/util/ArrayList;


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 13
    const-string v0, "n_onUtteranceCompleted:(Ljava/lang/String;)V:GetOnUtteranceCompleted_Ljava_lang_String_Handler:Android.Speech.Tts.TextToSpeech/IOnUtteranceCompletedListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n"

    sput-object v0, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;->__md_methods:Ljava/lang/String;

    .line 16
    const-class v1, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;

    const-string v2, "Android.Speech.Tts.TextToSpeech+IOnUtteranceCompletedListenerImplementor, Mono.Android"

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

    const-class v1, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;

    if-ne v0, v1, :cond_0

    .line 24
    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/Object;

    const-string v1, "Android.Speech.Tts.TextToSpeech+IOnUtteranceCompletedListenerImplementor, Mono.Android"

    const-string v2, ""

    invoke-static {v1, v2, p0, v0}, Lmono/android/TypeManager;->Activate(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Object;[Ljava/lang/Object;)V

    .line 26
    :cond_0
    return-void
.end method

.method private native n_onUtteranceCompleted(Ljava/lang/String;)V
.end method


# virtual methods
.method public monodroidAddReference(Ljava/lang/Object;)V
    .locals 1

    .line 39
    iget-object v0, p0, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;->refList:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 40
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;->refList:Ljava/util/ArrayList;

    .line 41
    :cond_0
    iget-object v0, p0, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;->refList:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 42
    return-void
.end method

.method public monodroidClearReferences()V
    .locals 1

    .line 46
    iget-object v0, p0, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;->refList:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 47
    invoke-virtual {v0}, Ljava/util/ArrayList;->clear()V

    .line 48
    :cond_0
    return-void
.end method

.method public onUtteranceCompleted(Ljava/lang/String;)V
    .locals 0

    .line 31
    invoke-direct {p0, p1}, Lmono/android/speech/tts/TextToSpeech_OnUtteranceCompletedListenerImplementor;->n_onUtteranceCompleted(Ljava/lang/String;)V

    .line 32
    return-void
.end method
