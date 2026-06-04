# Start from the same image you’re using now
FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

# Workaround: remove the broken Yarn APT source, if present
RUN rm -f /etc/apt/sources.list.d/yarn.list

# (Optional) If you actually need Yarn, re-add it correctly via keyring:
RUN apt-get update && apt-get install -y curl gnupg \
    && mkdir -p /usr/share/keyrings \
    && curl -sS https://dl.yarnpkg.com/debian/pubkey.gpg \
      | gpg --dearmor \
      | tee /usr/share/keyrings/yarnkey.gpg > /dev/null \
    && echo "deb [signed-by=/usr/share/keyrings/yarnkey.gpg] https://dl.yarnpkg.com/debian stable main" \
      | tee /etc/apt/sources.list.d/yarn.list