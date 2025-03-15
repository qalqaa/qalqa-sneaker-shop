REPLACEMENT_STRING=$1

find "." -type f -print0 | xargs -0 sed -i "s/<INSERT_DOMAIN>/$REPLACEMENT_STRING/g"

sudo docker compose -f ./docker-compose.https.yml run --rm certbot &&
  sudo docker compose -f ./docker-compose.https.yml stop &&
  sudo docker compose up -d
