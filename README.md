<h1 plerdy-tracking-id="26462577001">DNN Tokens module</h1>
<p>One of the most important aspects of content management: <strong>reliability</strong>. Your content needs to be 100% correct. But especially for larger or more complex websites, that can be a challenge. For example, if the gas price changes from 1.20 to 1.40, you want that update to be applied everywhere—without missing a forgotten landing page.</p>
<h3 plerdy-tracking-id="12623571301">Features</h3>
<h4 plerdy-tracking-id="93732681201">Simple text tokens</h4>
<p>You assign a token a name (e.g., <code>highTax</code>), a value (e.g., <code>21%</code>), and then use the token in content like this: <code>[{highTax}]</code>. If the high VAT rate changes to 21.5%, you only need to update it in one place: the token.</p>
<h3 plerdy-tracking-id="53736258301"><strong>One or multiple sites</strong></h3>
<p>DNN is particularly suited for running multiple websites within a single installation. With DNN Tokens, you can choose whether a token is active only in the site where it was created or across all websites in the installation.</p>
<h3 plerdy-tracking-id="45770764401"><strong>More complex text tokens</strong></h3>
<p>Beyond simple words or phrases (e.g., if you want to create a glossary), you can also include <strong>HTML</strong> in a text token. This allows you to manage elements like <strong>cards or carousels</strong> from one central location. To take it a step further, you can even include <code>&lt;script&gt;</code> and/or <code>&lt;style&gt;</code> tags. Essentially, this means you can reuse almost anything from <strong>CodePen</strong> in a way that doesn’t interfere with editors.</p>
<h4 plerdy-tracking-id="57235976801">Nested tokens</h4>
<p>You can use tokens within other tokens. For example, you can place a core DNN token like <code>[users:firstname]</code> inside a card and create a combined token like <code>[{personalizedCard}]</code>.</p>
<h4 plerdy-tracking-id="48305580101">Categories</h4>
<p>As the number of tokens grows, organization becomes key. You can create <strong>categories</strong> to structure your tokens. For instance, if you have multiple tax rates, you could create a <strong>Tax</strong> category. Your tokens would then be <code>[{tax:high}]</code> and <code>[{tax:low}]</code>.</p>
<h4 plerdy-tracking-id="35944987301">SQL&nbsp;</h4>
<p>Tokens can also be populated from a database. If you have a database of books, you could create a token like <code>[{book:name}]</code>, which executes an SQL query:</p>
<div class="contain-inline-size rounded-md border-[0.5px] border-token-border-medium relative bg-token-sidebar-surface-primary dark:bg-gray-950">
<div class="overflow-y-auto p-4" dir="ltr"><code class="!whitespace-pre language-sql"><span class="hljs-keyword">SELECT</span> name <span class="hljs-keyword">FROM</span> books <span class="hljs-keyword">WHERE</span> id <span class="hljs-operator">=</span> <span class="hljs-number">1</span></code></div>
</div>
<p>Since tokens can be nested, you can create a <strong>card</strong> or even an entire <strong>page</strong> where multiple tokens work together, displaying the book's name, cover, author, and intro.</p>
<h4 plerdy-tracking-id="12748689701">Razor</h4>
<p>If you can build a <strong>detail page</strong> using SQL, you'll also want a <strong>list view</strong> that links to those details.</p>
<h4 plerdy-tracking-id="69816871301">1 more thing...</h4>
<p>Several DNN ecosystem modules already use tokens—such as <strong>DNN core tokens, Live Tokens by Mandeeps, Plant an App tokens, 2SXC, and Easy DNN Solutions</strong>.</p>
<p>DNN Tokens is fully compatible with all of them. You can place DNN Tokens within <strong>2SXC content</strong>, <strong>Easy DNN</strong>, and even <strong>DNN Go skins</strong> that support token placement.</p>
<h2 plerdy-tracking-id="98935681301">Credits</h2>
<ul>
<li plerdy-tracking-id="33143264901"><a href="https://www.tjeps.com" target="_blank" rel="noopener" plerdy-tracking-id="72556370501"><strong>Tjep's digital agency</strong></a> conceived the concept, functionality, and funded the project.</li>
<li plerdy-tracking-id="72556370501"><a href="https://www.40fingers.net/" target="_blank" rel="noopener" plerdy-tracking-id="72556370501"><strong>40Fingers</strong></a> designed the architecture to ensure compatibility with other modules.</li>
<li plerdy-tracking-id="35985459201"><a href="https://www.easydnnsolutions.com/" target="_blank" rel="noopener" plerdy-tracking-id="35985459201"><strong>Easy DNN Solutions</strong></a> handled development and donated countless hours.</li>
</ul>
<h2 plerdy-tracking-id="22305083701">Costs</h2>
<p><strong>Free.</strong> Just like DNN itself, this module is released under the generous <strong>MIT license</strong>.</p>
<h2 plerdy-tracking-id="13097670001">Demo</h2>
<p><a href="https://www.youtube.com/watch?v=KfxoFYlAJZ4">https://www.youtube.com/watch?v=KfxoFYlAJZ4</a></p>
<h2 plerdy-tracking-id="10132487001"><strong>Download</strong></h2>
<p>You can download the module here:<br>🔗 <strong><a href="https://github.com/EasyDNNsolutions/DNNTokens/releases/" target="_new" rel="noopener" plerdy-tracking-id="85343233501">https://github.com/EasyDNNsolutions/DNNTokens/releases/</a></strong></p>
<h2 plerdy-tracking-id="81362521901">Support &amp; suggestions</h2>
<p>At <strong><a href="https://github.com/EasyDNNsolutions/DNNTokens" target="_new" rel="noopener" plerdy-tracking-id="33741646101">https://github.com/EasyDNNsolutions/DNNTokens</a></strong> you can fork the repository, modify the code, and submit a pull request.</p>
<p>Not a developer? No problem! You can report <strong>bugs or feature requests</strong> at:<br>🔗 <strong><a href="https://github.com/EasyDNNsolutions/DNNTokens/issues" target="_new" rel="noopener" plerdy-tracking-id="21196752301">https://github.com/EasyDNNsolutions/DNNTokens/issues</a></strong></p>
<p>(Some requests may require compensation.)</p>
<div class="mb-2 flex gap-3 empty:hidden -ml-2">
<div class="items-center justify-start rounded-xl p-1 flex" plerdy-tracking-id="29355997401">
<div class="flex items-center">&nbsp;</div>
</div>
</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
