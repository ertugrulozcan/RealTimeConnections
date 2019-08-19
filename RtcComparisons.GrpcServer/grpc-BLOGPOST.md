# gRPC

## Giriş

**gRPC**; HTTP/2 protokolü üzerinde çalışan, iki yönlü (bi-directional) iletişim kabiliyetine sahip, dil bağımsız (language-agnostic & Polyglot), yüksek performanslı, açık kaynaklı bir Uzak Prosedür Çağrısı (Remote Procedure Calling) framework'üdür. CNCF (Cloud Native Computing Foundation) adlı kuruluş tarafından geliştirilmektedir.

Google, uzunca bir süredir kendi servislerinde ve veri merkezlerinde kullandığı micro-servis omurgasını birbirine bağlamak için Stubby isimli bir rpc mimarisi kullanmaktaydı. Mart 2015'te Stubby'nin bir sonraki sürümü olarak geliştirimeye başlanan gRPC, Google desteği ve denetiminde CNCF tarafından açık kaynaklı olarak sürdürülmeye devam etmektedir.

Mimari olarak *Contract-First API* kategorisinde bulunur.

Varsayılan olarak ProtoBuf (Protocol Buffers) IDL'sini kullanır. (IDL : Interface Definition Language)
Ancak Google'ın yeni duyurduğu Flatbuffers teknolojisi, ProtoBuf'ın yerini alacak gibi görünmekte. Flatbuffers 1.7 sürümü ile birlikte gRPC desteği sunmaya da başlamıştır.

### gRPC Web Building

gRPC'de bir protobuf kontratından bir web uygulaması geliştirmek için kontratta tanımlı arayüzün javascript implementasyonu `proton-gen-grpc-web` uygulaması kullanılarak auto-generate olarak hazırlanabilir. Bu uygulama, verilen protobuf kodu üzerinden, proto'da tanımlanmış servis ve metodları, model sınıflarını ve data serialization gibi fonksiyonaliteleri içeren bir api tabanı hazırlar. Çıktı olarak servis adı önekiyle '_web_pb.js' ve '_pb.js' uzantılarına sahip iki adet javascript dosyası üretir. '_pb.js' uzantılı dosyada connection pool, serialization ve data streaming ile ilgili temel sınıflar yer alırken '_web_pb.js' uzantılı dosyada ise bu sınıfları kullanarak işlem yapan GrpcWebClientBase tabanlı bir client servisi ile model class'lar üretilir. Ayrıca servis içerisindeki metodlar da yine bu dosya içerisinde GrpcMethodDescriptor sınıfı ile üretilir.
Hazırlanan web api kodu bu api tabanı kullanılarak oluşturulur.

> `proton-gen-grpc-web` için indirme linki : https://github.com/grpc/grpc-web/releases

MacOS için uygulamanın sistem binary dizinine taşınması ve gerekli izinlerin verilmesi;
```
$ sudo mv ~/Downloads/protoc-gen-grpc-web-1.0.6-darwin-x86_64 \
$ /usr/local/bin/protoc-gen-grpc-web
$ chmod +x /usr/local/bin/protoc-gen-grpc-web
```

Build işlemi için kullanılan komutlar;

```
$ protoc -I=$SRC_DIR $PROTO_NAME.proto \
$ --js_out=import_style=commonjs:. \
$ --grpc-web_out=import_style=commonjs,mode=grpcwebtext:$OUTPUT_DIR
```

Uygulama dizininde çalıştırılan örnek shell script;
```
$ protoc -I=. grpctest.proto \
$ --js_out=import_style=commonjs:. \
$ --grpc-web_out=import_style=commonjs,mode=grpcwebtext:.
```

___
___

### HTTP vs gRPC

.                | gRPC                       | HTTP APIs
---------------- | -------------------------- | --------------------------
Kontrat          | *.proto                    | Opsiyonel (Örn. OpenAPI)
Protokol         | HTTP/2                     | HTTP
Payload          | Binary                     | Metinsel (+Human readable)
Paradigma        | Kurallı                    | Zayıf
Streaming        | İki yönlü (bi-directional) | Request-Response Challenge
Tarayıcı Desteği | Tam destek mevcut değil    | Tüm tarayıcılar
Güvenlik         | HTTPS                      | HTTPS

HTTP ve HTTP/2 sürümleri arasındaki performans karşılaştırması : http://www.http2demo.io/

___

### gRPC vs SignalR

> Broadcast real-time communication – gRPC supports real-time communication via streaming, but the concept of broadcasting a message out to registered connections doesn't exist. For example in a chat room scenario where new chat messages should be sent to all clients in the chat room, each gRPC call is required to individually stream new chat messages to the client. SignalR is a useful framework for this scenario. SignalR has the concept of persistent connections and built-in support for broadcasting messages.