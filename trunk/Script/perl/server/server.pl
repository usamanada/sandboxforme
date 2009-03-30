#!/usr/bin/perl

use IO::Socket;
use IO::Select;
$SIG='IGNORE';
$m=new IO::Socket::INET(Listen=>1,LocalPort=>8888,Reuse=>1);
$O=new IO::Select($m);
$/="";
while(@S=$O->can_read){
    foreach(@S){
        if($_==$m){
            $C=$m->accept;$O->add($C);
            warn "connected";
        }else{
            my $R=sysread($_, $i, 2048);

		# warn $i; # buf
		# warn $R; # n
		# warn $_;

            if($R==0){
                $T=syswrite($_, '', 2048);
                if($T==undef){
                    $O->remove($_);
                    warn "disconnected";
                }
            }else{
                foreach $C($O->can_write()){
                # foreach $C($O->handles){
			# warn $i;
			# warn $C;
                     $T=syswrite($C, $i,2048);
                        warn $i;
			# warn $T;
                }
            }
        }
    }
}
