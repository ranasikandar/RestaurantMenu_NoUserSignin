<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrivacyPolicy.ascx.cs" Inherits="RestaurantMenu.Restaurant.UC.PrivacyPolicy" %>
<div class="modal-dialog modal-lg">
    <style>
        .bold{font-weight: bold;}
    </style>
    <div class="modal-content">
        <div class="modal-header">
            <h4 class="modal-title"><% Response.Write(System.Configuration.ConfigurationManager.AppSettings["BusinessName"].ToString());%> Privacy Policy</h4>
            <button type="button" class="close" data-dismiss="modal">
                <span aria-hidden="true">×</span>
                <span class="sr-only">close</span>
            </button>
        </div>
        <div class="modal-body">
            <p class="text-center bold">Informativa Privacy</p>
            <br />
            <p>Lo scopo del presente documento (di seguito “Informativa Privacy”) è di informare gli Utenti sui dati personali, intesi come qualsiasi informazione che permette l’identifcazione di una persona fsica (di seguito i “Dati Personali”), raccolti dal sito web Lebe-Venice.com (di seguito “Applicazione”).</p>
            <p>Il Titolare del Trattamento, come successivamente identifcato, potrà modifcare o semplicemente aggiornare, in tutto o in parte, la presente Informativa dandone informazione agli Utenti. Le modifche e gli aggiornamenti saranno vincolanti non appena pubblicati sull’Applicazione. L'Utente è pertanto invitato a leggere l’Informativa Privacy ad ogni accesso all’Applicazione.</p>
            <p>Nel caso di mancata accettazione delle modifche apportate all’Informativa Privacy, l’Utente è tenuto a cessare l’utilizzo di questa Applicazione e può richiedere al Titolare del Trattamento di rimuovere i propri Dati Personali.</p>
            <p class="bold">1. Dati Personali raccolti dall’Applicazione</p>
            <p>Il Titolare raccoglie le seguenti tipologie di Dati Personali:</p>
            <p class="bold">A. Contenuti e informazioni forniti volontariamente dall’Utente</p>
            <p><span class="la la-circle"></span> <span class="bold">di contatto e contenuti:</span> sono quei Dati Personali che l’Utente volontariamente fornisce all’Applicazione durante il suo utilizzo, quali ad esempio dati anagrafci, recapiti, credenziali di accesso ai servizi e/o prodotti forniti, interessi e preferenze personali e altri contenuti personali, etc.</p>
            
            <p><span class="la la-circle"></span> <span class="bold">Dati personali raccolti dai social media:</span> gli Utenti possono condividere con l’Applicazione dati forniti ai social media. L’Utente ha facoltà di controllare i Dati Personali a cui l’Applicazione può accedere tramite le impostazioni sulla privacy disponibili nei social media in questione. Associando account gestiti da social media con l’Applicazione e autorizzando il Titolare ad accedere a tali Dati Personali, l’Utente presta consenso all'acquisizione, al trattamento e alla loro conservazione in conformità alla presente Informativa Privacy.</p>
            <p>Il mancato conferimento da parte dell’Utente dei Dati Personali, per i quali sussiste l’obbligo legale, contrattuale o qualora costituiscano requisito necessario per l’utilizzo del servizio o per la conclusione del contratto, comporterà l’impossibilità del Titolare di erogare in tutto o in parte i propri servizi.</p>
            <p>L’Utente che comunichi al Titolare Dati Personali di terzi è direttamente ed esclusivamente responsabile della loro provenienza, raccolta, trattamento, comunicazione o diffusione.</p>
            <p class="bold">B. Dati e contenuti acquisiti automaticamente durante l’utilizzo dell’Applicazione:</p>
            <p><span class="la la-circle"></span> <span class="bold">Dati tecnici:</span> i sistemi informatici e le procedure software preposte al funzionamento di questa Applicazione possono acquisire, nel corso del loro normale esercizio, alcuni Dati Personali la cui trasmissione è implicita nell’uso dei protocolli di comunicazione internet. Si tratta di informazioni che non sono raccolte per essere associate a Utenti identifcati, ma che per la loro stessa natura, potrebbero, attraverso elaborazioni ed associazioni con Dati detenuti da terzi, permettere di identifcare gli Utenti. In questa categoria rientrano gli indirizzi IP, o i nomi di dominio utilizzati dagli Utenti che si connettono all’Applicazione, gli indirizzi in notazione URI (Uniform Resource Identifer) delle risorse richieste, l’orario della richiesta, il metodo utilizzato nel sottoporre la richiesta al server, la dimensione del file ottenuto, etc.</p>
            <p><span class="la la-circle"></span> <span class="bold">Dati di utilizzo:</span> possono essere raccolti anche Dati Personali relativi all’utilizzo dell’Applicazione da parte dell’Utente, quali ad esempio le pagine visitate, le azioni compiute, le funzionalità e i servizi utilizzati.</p>
            <p><span class="la la-circle"></span> <span class="bold">Dati di geolocalizzazione:</span> l’Applicazione può raccogliere Dati Personali sulla posizione dell’Utente che possono essere dati GNSS (Global Navigation Satellite System, ad esempio quelli GPS), oltre ai dati che identifcano il ripetitore più vicino, gli hotspot Wi-Fi e bluetooth, comunicati quando si abilitano i prodotti o le funzionalità basati sulla posizione.</p>
            <p class="bold">C. Dati personali raccolti tramite cookie o tecnologie simili:</p>
            <p>L’Applicazione usa cookie, web beacon, identifcatori univoci e altre analoghe tecnologie per raccogliere Dati Personali sulle pagine, sui collegamenti visitati e sulle altre azioni che si eseguono quando si utilizzano i nostri servizi. Essi vengono memorizzati per essere poi ritrasmessi alla successiva visita del medesimo Utente.</p>
            <p>L’Utente può prendere visione della Cookie Policy completa al seguente indirizzo: www.lebe-venice.com/cookie-policy.</p>
            <p class="bold">2. Finalità</p>
            <p>I Dati Personali raccolti possono essere utilizzati per l’esecuzione di obblighi contrattuali e precontrattuali e per obblighi di legge.</p>
            <p class="bold">3. Modalità di trattamento</p>
            <p>Il trattamento dei Dati Personali viene e>ettuato mediante strumenti informatici e/o telematici, con modalità organizzative e con logiche strettamente correlate alle finalità indicate.</p>
            <p>In alcuni casi potrebbero avere accesso ai Dati Personali anche soggetti coinvolti nell’organizzazione del Titolare (quali per esempio, addetti alla gestione del personale, addetti all’area commerciale, amministratori di sistema, etc.) ovvero soggetti esterni (come società informatiche, fornitori di servizi, corrieri postali, hosting provider, etc.). Detti soggetti all’occorrenza potranno essere nominati Responsabili del Trattamento da parte del Titolare, nonché accedere ai Dati Personali degli Utenti ogni qualvolta si renda necessario e saranno contrattualmente obbligati a mantenere riservati i Dati Personali</p>
            <p>L’elenco aggiornato dei Responsabili può essere richiesto via email all’indirizzo info@lebe-venice.com</p>
            <p class="bold">4. Base giuridica del trattamento</p>
            <p>Il trattamento dei Dati Personali relativi all’Utente si fonda sulle seguenti basi giuridiche:</p>
            <p><span class="la la-circle"> </span> il consenso prestato dall’Utente per una o più finalità specifiche;</p>
            <p><span class="la la-circle"> </span> il trattamento è necessario all'esecuzione di un contratto con l’Utente e/o all'esecuzione di misure precontrattuali</p>
            <p><span class="la la-circle"> </span> il trattamento è necessario per adempiere un obbligo legale al quale è soggetto il Titolare</p>
            <p><span class="la la-circle"> </span> il trattamento è necessario per l'esecuzione di un compito di interesse pubblico o per l'esercizio di pubblici poteri di cui è investito il Titolare</p>
            <p><span class="la la-circle"> </span> il trattamento è necessario per il perseguimento del legittimo interesse del Titolare o di terzi</p>
            <p><span class="la la-circle"> </span> il trattamento è necessario per il perseguimento di un interesse vitale del Titolare o di terzi.</p>
            <p>È comunque sempre possibile richiedere al Titolare di chiarire la base giuridica di ciascun trattamento all’indirizzo info@lebevenice.com</p>
            <p class="bold">5. Luogo</p>
            <p>I Dati Personali sono trattati presso le sedi operative del Titolare ed in ogni altro luogo in cui le parti coinvolte nel trattamento siano localizzate. Per ulteriori informazioni, contatta il Titolare al seguente indirizzo email info@lebe-venice.com oppure al seguente indirizzo postale info@lebe-venice.com.</p>
            <p class="bold">6. Misure di sicurezza</p>
            <p>Il Trattamento viene e>ettuato secondo modalità e con strumenti idonei a garantire la sicurezza e la riservatezza dei Dati Personali, avendo il Titolare adottato misure tecniche e organizzative adeguate che garantiscono, e consentono di dimostrare, che il Trattamento è effettuato in conformità alla normativa di riferimento.</p>
            <p class="bold">7. Periodo di conservazione dei Dati</p>
            <p>I Dati Personali saranno conservati per il periodo di tempo necessario ad adempiere alle finalità per i quali sono stati raccolti.</p>
            <p>In particolare, i Dati Personali saranno conservati per tutta la durata del rapporto contrattuale, per l’esecuzione degli adempimenti allo stesso inerenti e conseguenti, per il rispetto degli obblighi di legge e regolamentari applicabili, nonché per fnalità difensive proprie o di terzi.</p>
            <p>Qualora il trattamento dei Dati Personali sia basato sul consenso dell’Utente, il Titolare può conservare i Dati Personali sino alla revoca del consenso.</p>
            <p>I Dati Personali possono essere conservati per un periodo più lungo se necessari per adempiere un obbligo di legge o per ordine di un’autorità.</p>
            <p>Tutti i Dati Personali saranno cancellati o conservati in una forma che non consenta l’identifcazione dell’Utente entro 30 giorni dal termine del periodo di conservazione. Allo scadere di tale termine il diritto di accesso, cancellazione, rettifca ed il diritto alla portabilità dei Dati Personali non potranno più essere esercitati.</p>
            <p class="bold">8. Processi decisionali automatizzati</p>
            <p>Tutti i Dati Personali raccolti non saranno oggetto di alcun processo decisionale automatizzato, compresa la proflazione, che possa produrre effetti giuridici per la persona o che possa incidere su di essa in modo significativo.</p>
            <p class="bold">9. Diritti dell’Utente</p>
            <p>Gli Utenti possono esercitare determinati diritti con riferimento ai Dati Personali trattati dal Titolare. In particolare, l’Utente ha il diritto di:</p>
            <p><span class="la la-circle"> </span> revocare il consenso in ogni momento;</p>
            <p><span class="la la-circle"> </span> opporsi al trattamento dei propri Dati Personali;</p>
            <p><span class="la la-circle"> </span> accedere ai propri Dati Personali;</p>
            <p><span class="la la-circle"> </span> verificare e chiedere la rettifica;</p>
            <p><span class="la la-circle"> </span> ottenere la limitazione del trattamento;</p>
            <p><span class="la la-circle"> </span> ottenere la cancellazione dei propri Dati Personali;</p>
            <p><span class="la la-circle"> </span> ricevere i propri Dati Personali o farli trasferire ad altro titolare;</p>
            <p><span class="la la-circle"> </span> proporre reclamo all’autorità di controllo della protezione dei Dati Personali e/o agire in sede giudiziale.</p>
            <p>Per esercitare i propri diritti, gli Utenti possono indirizzare una richiesta agli estremi di contatto del Titolare indicati in questo documento. Le richieste sono effettuate a titolo gratuito ed evase dal Titolare nel più breve tempo possibile, in ogni caso entro 30 giorni.</p>
            <p class="bold">10. Titolare del Trattamento</p>
            <p>Il Titolare del Trattamento è Daniele Marton, Via principe 135, musestre, Codice Fiscale MRTDNL98H04L407T, Partita IVA 04870020262, indirizzo e-mail info@lebe-venice.com, indirizzo PEC martondaniele@pec.it</p>
            <p>Ultimo aggiornamento: 04/12/2019</p>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-shadow" data-dismiss="modal">Close</button>
        </div>
    </div>
</div>
